lexer grammar ExpressionLexer;

STARTSTRING                 : '\''                           -> pushMode(INSIDESTRING) ;
EXITINTERP                  : '}'                            -> popMode ;
AND                         : '&&' ;
OR                          : '||' ;
NOT                         : '!' ;
TRUE                        : 'true' ;
FALSE                       : 'false' ;
GT                          : '>' ;
GTE                         : '>=' ;
LT                          : '<' ;
LTE                         : '<=' ;
EQ                          : '==' ;
COMMA                       : ',' ;
DOT                         : '.' ;
LPAREN                      : '(' ;
RPAREN                      : ')' ;
PLUS                        : '+' ;
MINUS                       : '-' ;
TIMES                       : '*' ;
DIVIDE                      : '/' ;
MOD                         : '%' ;
NUMERIC                     : '-'? [0-9]+ ('.' [0-9]+)? ;
IDENTIFIER                  : [a-zA-Z_] [a-zA-Z_0-9]* ;
WS                          : [ \r\t\u000C\n]+ -> skip;

// --- Inside String ---
mode INSIDESTRING;

CLOSESTRING                 : '\''                           -> popMode ;
CLOSEINTERP                 : '{'                            -> pushMode(default) ;
ESCAPESTRING                : '\\\''
                            | '\\\\'
                            | '\\}'
                            | '\\{'
                            ;
DETAILSSTRING               : ~['\\{}]+ ;