lexer grammar ExpressionLexer;

COMMA                       : ',' ;
DOLLAR                      : '$' ;
LPARAM                      : '(' ;
RPARAM                      : ')' ;
INTEGER                     : Minus? Digit+ ;
DECIMAL                     : Minus? Digit+ (Dot Digit+)? ;
STRING                      : '"' ~["]* '"'
                            | '\'' ~[']* '\''
                            ;
TRUE                        : T R U E ;
FALSE                       : F A L S E ;
EVENT                       : E V E N T ;
IDENTIFIER                  : StartCharacter NameChar* ;
WS                          : [ \t\r\n]+ -> skip ;

// --- Fragments ---
fragment Minus              : '-' ;
fragment Dot                : '.' ;
fragment Digit              : [0-9] ;
fragment T                  : [Tt] ;
fragment R                  : [Rr] ;
fragment U                  : [Uu] ;
fragment E                  : [Ee] ;
fragment F                  : [Ff] ;
fragment A                  : [Aa] ;
fragment L                  : [Ll] ;
fragment S                  : [Ss] ;
fragment V                  : [Vv] ;
fragment N                  : [Nn] ;
fragment StartCharacter     : [A-Z] ;
fragment NameChar           : StartCharacter | [a-z] | Digit ;