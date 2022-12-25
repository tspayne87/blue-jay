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
 | FOR scope=forScope FORIN id=forIdentifier+ FORCLOSE                                                    #forattribute
 | REF ref=REFNAME REFCLOSE                                                                               #refattribute
 | STYLEATTR syl=style STYLECLOSE                                                                         #styleattribute
 | name=EXPRESSIONATTR expr=scopeExpression EXPCLOSE                                                      #exprattribute
 | name=EVENTATTR expr=scopeExpression EXPCLOSE                                                           #eventattribute
 | name=STRATTR str=ATTRDETAILSSTRING ATTRCLOSESTRING                                                     #stringattribute
 ;
forScope
 : FORHASH FORIDENTIFIER
 ;
forIdentifier
 : id=FORIDENTIFIER                                                                                       #simpleForIdentifier
 | id=FORIDENTIFIER FOROPENSQUAREBRACKET expr=scopeExpression FORCLOSESQUAREBRACKET                       #simpleForArrayIdentifier
 | FORDOT id=FORIDENTIFIER                                                                                #dotForIdentifier
 | FORDOT id=FORIDENTIFIER FOROPENSQUAREBRACKET expr=scopeExpression FOROPENSQUAREBRACKET                 #arrayForIdentifier
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
 | HASH identifier=IDENTIFIER                                                                             #scopeIdentifierExpression
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
 : styleItem (SEMICOLON styleItem)* ;
styleItem
 : name=WIDTH STYLECOLON (styleInteger | styleExpression)
 | name=WIDTHPERCENTAGE STYLECOLON (styleDecimal | styleExpression)
 | name=HEIGHT STYLECOLON (styleInteger | styleExpression)
 | name=HEIGHTPERCENTAGE STYLECOLON (styleDecimal | styleExpression)
 | name=TOPOFFSET STYLECOLON (styleInteger | styleExpression)
 | name=LEFTOFFSET STYLECOLON (styleInteger | styleExpression)
 | name=PADDING STYLECOLON (styleInteger | styleExpression)
 | name=HORIZONTALALIGN STYLECOLON (horizontalAlign | styleExpression)
 | name=VERTICALALIGN STYLECOLON (verticalAlign | styleExpression)
 | name=POSITION STYLECOLON (position | styleExpression)
 | name=NINEPATCH STYLECOLON (styleNinePatch | styleExpression)
 | name=TEXTCOLOR STYLECOLON (styleColor | styleExpression)
 | name=BACKGROUNDCOLOR STYLECOLON (styleColor | styleExpression)
 | name=TEXTALIGN STYLECOLON (textAlign | styleExpression)
 | name=TEXTBASELINE STYLECOLON (textBaseline | styleExpression)
 | name=GRIDCOLUMNS STYLECOLON (styleInteger | styleExpression)
 | name=COLUMNGAP STYLECOLON (stylePoint | styleExpression)
 | name=COLUMNSPAN STYLECOLON (styleInteger | styleExpression)
 | name=COLUMNOFFSET STYLECOLON (styleInteger | styleExpression)
 | name=FONT STYLECOLON (styleWord | styleExpression)
 | name=TEXTUREFONT STYLECOLON (styleWord | styleExpression)
 | name=TEXTUREFONTSIZE STYLECOLON (styleInteger | styleExpression)
 | name=HEIGHTTEMPLATE STYLECOLON (heightTemplate | styleExpression)
 ;
styleExpression
 : STYLEOPENEXPRESSION expr=scopeExpression CLOSEBRACKETS
 ;
styleDecimal
 : DECIMAL
 | INTEGER
 ;
styleInteger
 : INTEGER
 ;
styleNinePatch
 : WORD
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

// --- Enumerations ---
horizontalAlign
 : RIGHT
 | CENTER
 | LEFT
 ;
verticalAlign
 : TOP
 | CENTER
 | BOTTOM
 ;
textAlign
 : RIGHT
 | CENTER
 | LEFT
 ;
textBaseline
 : TOP
 | CENTER
 | BOTTOM
 ;
position
 : RELATIVE
 | ABSOLUTE
 ;
heightTemplate
 : STRETCH
 ;