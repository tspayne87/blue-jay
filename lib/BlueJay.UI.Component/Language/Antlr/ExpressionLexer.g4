lexer grammar ExpressionLexer;

COMMA                       : ',' ;
DOLLAR                      : '$' ;
LPARAM                      : '(' ;
RPARAM                      : ')' ;
DOT                         : '.' ;
INTEGER                     : Minus? Digit+ ;
DECIMAL                     : Minus? Digit+ (Dot Digit+)? ;
STRING                      : '"' ~["]* '"'
                            | '\'' ~[']* '\''
                            ;
TRUE                        : T R U E ;
FALSE                       : F A L S E ;
IDENTIFIER                  : StartCharacter NameChar* ;
SCOPEIDENTIFIER             : ScopeStartCharacter NameChar* ;
WS                          : [ \t\r\n]+ -> skip ;

// --- Fragments ---
fragment Minus                : '-' ;
fragment Dot                  : '.' ;
fragment Digit                : [0-9] ;
fragment T                    : [Tt] ;
fragment R                    : [Rr] ;
fragment U                    : [Uu] ;
fragment E                    : [Ee] ;
fragment F                    : [Ff] ;
fragment A                    : [Aa] ;
fragment L                    : [Ll] ;
fragment S                    : [Ss] ;
fragment StartCharacter       : [A-Z] ;
fragment Lower                : [a-z] ;
fragment ScopeStartCharacter  : StartCharacter | Lower ;
fragment NameChar             : StartCharacter | Lower | Digit ;