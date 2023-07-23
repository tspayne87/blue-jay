parser grammar JayTMLParser;

options { tokenVocab=JayTMLLexer; }

document
 : COMMENT? root=content EOF
 ;
content
 : str=chardata                                                                                           #stringContent
 | ele=element                                                                                            #elementContent
 | COMMENT                                                                                                #commentContent
 ;
element
 : OPEN name=NAME attribute* CLOSE content* OPEN SLASH closename=NAME CLOSE                               #treeElement
 | OPEN name=NAME attribute* SLASH_CLOSE                                                                  #simpleElement
 | OPEN SLOT SLASH_CLOSE                                                                                  #slotElement
 ;
attribute
 : IF expr=scopeExpression EXPCLOSE                                                                       #ifattribute
 | FOR scope=forScope FORIN exp=forInExpression FORCLOSE                                                  #forattribute
 | REF ref=REFNAME REFCLOSE                                                                               #refattribute
 | STYLEATTR syl=style STYLECLOSE                                                                         #styleattribute
 | name=EXPRESSIONATTR expr=scopeExpression EXPCLOSE                                                      #exprattribute
 | name=EVENTATTR expr=scopeExpression EXPCLOSE                                                           #eventattribute
 | name=STRATTR str=ATTRDETAILSSTRING ATTRCLOSESTRING                                                     #stringattribute
 ;
forScope
 : FORHASH FORIDENTIFIER
 ;
forInExpression
 : left=forConstant FORDOUBLEDOT right=forConstant                                                        #forRangeExpression
 | forConstant                                                                                            #forConstantExpression
 ;
forConstant
 : FORINTEGER                                                                                             #forInteger
 | FOROPENEXPRESSION expr=scopeExpression CLOSEBRACKETS                                                   #forExpression
 ;
chardata
 : (TEXT | contentExpression)+ ;
contentExpression
 : OPENEXPRESSION expr=scopeExpression CLOSEBRACKETS
 ;
scopeExpression
 : binaryExpression
 ;
binaryExpression
 : left=expression (op=binary right=binaryExpression)?
 ;
expression
 : LPAREN expr=expression RPAREN                                                                          #parenExpression
 | NOT expr=expression                                                                                    #notExpression
 | left=expression op=comparator right=expression                                                         #comparatorExpression
 | left=expression op=arithmetic right=expression                                                         #arithmeticExpression
 | expr=bool                                                                                              #boolExpression
 | method=IDENTIFIER LPAREN (expression (COMMA expression)*)? RPAREN                                      #invokeMethodExpression
 | DOLLAR IDENTIFIER                                                                                      #eventIdentifierExpression
 | HASH identifier=IDENTIFIER (DOT IDENTIFIER)*                                                           #scopeIdentifierExpression
 | identifier+                                                                                            #identifierExpression
 | num=NUMERIC                                                                                            #numericExpression
 | STARTSTRING stringDetails+ CLOSESTRING                                                                 #stringExpression
 ;
identifier
 : id=IDENTIFIER                                                                                          #simpleIdentifier
 | id=IDENTIFIER OPENSQUAREBRACKET expr=scopeExpression CLOSESQUAREBRACKET                                #simpleArrayIdentifier
 | DOT id=IDENTIFIER                                                                                      #dotIdentifier
 | DOT id=IDENTIFIER OPENSQUAREBRACKET expr=scopeExpression CLOSESQUAREBRACKET                            #arrayIdentifier
 ;
comparator
 : GT | GTE | LT | LTE | EQ
 ;
binary
 : AND | OR
 ;
arithmetic
 : PLUS | MINUS | TIMES | DIVIDE | MOD
 ;
bool
 : TRUE | FALSE
 ;
stringDetails
 : escape=ESCAPESTRING                                                                                    #stringEscapeExpression
 | details=DETAILSSTRING                                                                                  #stringDetailsExpression
 | CLOSEINTERP expr=expression EXITINTERP                                                                 #stringInterpExpression
 ;
style
 : styleItem (SEMICOLON styleItem)* SEMICOLON? ;
styleItem
 : name=WORD alternate=styleAlternate (styleInteger | styleDecimal | styleWord | styleColor | stylePoint | styleExpression)
 ;
styleAlternate
 : STYLEDOUBLECOLON type=HOVER STYLECOLON
 | STYLECOLON
 ;
styleExpression
 : STYLEOPENEXPRESSION expr=scopeExpression CLOSEBRACKETS
 ;
styleDecimal
 : DECIMAL
 ;
styleInteger 
 : INTEGER
 ;
styleColor
 : r=styleInteger STYLECOMMA g=styleInteger STYLECOMMA b=styleInteger (STYLECOMMA a=styleInteger)?
 ;
stylePoint
 : x=styleInteger (STYLECOMMA y=styleInteger)?
 ;
styleWord
 : WORD
 ;