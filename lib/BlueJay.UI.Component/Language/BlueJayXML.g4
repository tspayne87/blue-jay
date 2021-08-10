grammar BlueJayXML;
prog                        : elementExpression EOF ;

elementExpression           : STARTELEMENT elementNameExpression ((WS)+ attributeExpression)* (WS)* ENDSINGLE
                            | STARTELEMENT elementNameExpression ((WS)+ attributeExpression)* ENDELEMENT textExpression ENDSTART elementNameExpression ENDELEMENT
                            ;
elementNameExpression       : LOWER (DIGIT | LOWER | MINUS)*
                            ;
attributeExpression         : stringAttributeExpression
                            ;
stringAttributeExpression   : attributeNameExpression EQUALS SINGLEQUOTE textExpression SINGLEQUOTE
                            | attributeNameExpression EQUALS DOUBLEQUOTE textExpression DOUBLEQUOTE
                            ;
attributeNameExpression     : LOWER (DIGIT | LOWER | MINUS)*
                            ;
textExpression              : (UPPER | LOWER | DIGIT | WS)*
                            ;

// Single tokens
EQUALS            : '=' ;
ENDELEMENT        : '>' ;
STARTELEMENT      : '<' ;
DOUBLEQUOTE       : '"' ;
SINGLEQUOTE       : '\'' ;
MINUS             : '-' ;
LOWER             : [a-z] ;
UPPER             : [A-Z] ;
DIGIT             : [0-9] ;
WS                : [ \t\r\n] ;

// Multi token
ENDSINGLE         : '/>' ;
ENDSTART          : '</' ;

// Case Insensitive found at https://github.com/antlr/antlr4/blob/master/doc/case-insensitive-lexing.md
fragment A : [aA] ;
fragment B : [bB] ;
fragment C : [cC] ;
fragment D : [dD] ;
fragment E : [eE] ;
fragment F : [fF] ;
fragment G : [gG] ;
fragment H : [hH] ;
fragment I : [iI] ;
fragment J : [jJ] ;
fragment K : [kK] ;
fragment L : [lL] ;
fragment M : [mM] ;
fragment N : [nN] ;
fragment O : [oO] ;
fragment P : [pP] ;
fragment Q : [qQ] ;
fragment R : [rR] ;
fragment S : [sS] ;
fragment T : [tT] ;
fragment U : [uU] ;
fragment V : [vV] ;
fragment W : [wW] ;
fragment X : [xX] ;
fragment Y : [yY] ;
fragment Z : [zZ] ;