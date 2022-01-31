lexer grammar ForLexer;

DOLLAR  : '$' ;
IN    : 'in' ;
SCOPENAME : StartCharIdentifier (CharIdentifier+)? ;
TEXT  : .+? ;

// --- Fragments ---
fragment Underscore          : '_' ;
fragment Digit               : [0-9] ;
fragment Upper               : [A-Z] ;
fragment Lower               : [a-z] ;
fragment StartCharIdentifier : Upper | Lower | Underscore ;
fragment CharIdentifier      : StartCharIdentifier | Digit ;