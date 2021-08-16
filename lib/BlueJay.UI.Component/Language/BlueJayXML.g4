grammar BlueJayXML;
prog                        : elementExpression EOF ;

elementExpression           : containerExpression
                            | slotExpression
                            | customElementExpression
                            ;
containerExpression         : STARTELEMENT CONTAINER ((WS)+ attributeExpression)* (WS)* ENDSINGLE
                            | STARTELEMENT CONTAINER ((WS)+ attributeExpression)* ENDELEMENT innerExpression ENDSTART CONTAINER ENDELEMENT
                            ;
slotExpression              : STARTELEMENT SLOT ((WS)+ attributeExpression)* (WS)* ENDSINGLE
                            ;
customElementExpression     : STARTELEMENT identifier ((WS)+ attributeExpression)* (WS)* ENDSINGLE
                            | STARTELEMENT identifier ((WS)+ attributeExpression)* ENDELEMENT innerExpression ENDSTART identifier ENDELEMENT
                            ;
innerExpression             : textExpression (WS)* elementExpression*
                            | elementExpression (WS)* elementExpression*
                            ;
attributeExpression         : stringAttributeExpression
                            | bindingAttributeExpression
                            | eventAttributeExpression
                            ;
stringAttributeExpression   : identifier EQUALS SINGLEQUOTE textExpression SINGLEQUOTE
                            | identifier EQUALS DOUBLEQUOTE textExpression DOUBLEQUOTE
                            ;
bindingAttributeExpression  : COLON identifier EQUALS SINGLEQUOTE basicExpression SINGLEQUOTE
                            | COLON identifier EQUALS DOUBLEQUOTE basicExpression DOUBLEQUOTE
                            ;
eventAttributeExpression    : AT identifier (DOT GLOBAL)? EQUALS SINGLEQUOTE functionExpression SINGLEQUOTE
                            | AT identifier (DOT GLOBAL)? EQUALS DOUBLEQUOTE functionExpression DOUBLEQUOTE
                            ;
textExpression              : (UPPER | LOWER | DIGIT | WS | CONTAINER | FALSE | TRUE | EVENT | GLOBAL)*
                            ;
basicExpression             : literalExpression
                            | functionExpression
                            | identifier
                            | contextVarExpression
                            ;
literalExpression           : string
                            | decimal
                            | integer
                            | boolean
                            ;
functionExpression          : identifier LPARAM argumentExpression RPARAM
                            ;
contextVarExpression        : DOLLAR EVENT
                            ;
argumentExpression          : basicExpression (WS* COMMA WS* basicExpression)*
                            ;
identifier                  : (LOWER | UPPER | UNDERSCORE) (LOWER | UPPER | DIGIT)*
                            ;
decimal                     : MINUS? DIGIT+ DOT DIGIT+
                            ;
integer                     : MINUS? DIGIT+
                            ;
string                      : SINGLEQUOTE textExpression SINGLEQUOTE
                            | DOUBLEQUOTE textExpression DOUBLEQUOTE
                            ;
boolean                     : TRUE
                            | FALSE
                            ;

// Single tokens
EQUALS            : '=' ;
ENDELEMENT        : '>' ;
STARTELEMENT      : '<' ;
DOUBLEQUOTE       : '"' ;
SINGLEQUOTE       : '\'' ;
MINUS             : '-' ;
COLON             : ':' ;
UNDERSCORE        : '_' ;
DOLLAR            : '$' ;
LPARAM            : '(' ;
RPARAM            : ')' ;
COMMA             : ',' ;
QUESTION          : '?' ;
DOT               : '.' ;
AT                : '@' ;
LOWER             : [a-z] ;
UPPER             : [A-Z] ;
DIGIT             : [0-9] ;
WS                : [ \t\r\n] ;

// Multi token
ENDSINGLE         : '/>' ;
ENDSTART          : '</' ;
EVENT             : E V E N T ;
TRUE              : T R U E ;
FALSE             : F A L S E ;
CONTAINER         : C O N T A I N E R ;
SLOT              : S L O T ;
GLOBAL            : G L O B A L ;

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