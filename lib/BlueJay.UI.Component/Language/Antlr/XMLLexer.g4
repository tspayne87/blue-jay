lexer grammar XMLLexer;

// --- Basic Processing ---
COMMENT       : '<!--' .*? '-->' ;
OPEN          : '<'                                     -> pushMode(INSIDE) ;
SEA_WS        :   (' ' | '\t' | '\r'? '\n')+ ;
EXPRESSION    : '{{' ~[}]+ '}}' ;
TEXT          : ~[<&{]+ ;


// --- Inside Tag ---
mode INSIDE;

CLOSE                       : '>'                       -> popMode ;
SLASH_CLOSE                 : '/>'                      -> popMode ;
SLASH                       : '/' ;
COLON                       : ':' ;
AT                          : '@' ;
EQUALS                      : '=' ;
DOT                         : '.' ;
STRING                      : '"' ~[<"]* '"'
                            | '\'' ~[<']* '\''
                            ;
IF                          : 'if' ;
FOR                         : 'for' ;
SLOT                        : 'Slot' ;
GLOBAL                      : 'Global' ;
NAME                        : StartCharacter NameChar* ;
WS                          : [ \t\r\n]+ -> skip ;

// --- Fragments Inside tag ---
fragment Digit              : [0-9] ;
fragment StartCharacter     : [A-Z] ;
fragment NameChar           : StartCharacter | [a-z] | Digit ;