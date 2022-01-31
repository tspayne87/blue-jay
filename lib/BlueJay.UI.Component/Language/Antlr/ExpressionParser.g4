parser grammar ExpressionParser;

options { tokenVocab=ExpressionLexer; }

parse
  : binaryExpression EOF
  ;

binaryExpression
 : left=expression (op=binary right=binaryExpression)?
 ;

expression
 : LPAREN expr=expression RPAREN                                              #parenExpression
 | NOT expr=expression                                                        #notExpression
 | left=expression op=comparator right=expression                             #comparatorExpression
 | left=expression op=arithmetic right=expression                             #arithmeticExpression
 | expr=bool                                                                  #boolExpression
 | method=IDENTIFIER LPAREN (expression (COMMA expression))? RPAREN           #invokeMethodExpression
 | IDENTIFIER (DOT IDENTIFIER)?                                               #identifierExpression
 | num=NUMERIC                                                                #numericExpression
 | STARTSTRING stringDetails+ CLOSESTRING                                     #stringExpression
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
 : escape=ESCAPESTRING                                                        #stringEscapeExpression
 | details=DETAILSSTRING                                                      #stringDetailsExpression
 | CLOSEINTERP expr=expression EXITINTERP                                     #stringInterpExpression
 ;