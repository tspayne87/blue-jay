lexer grammar ForLexer;

DOLLAR  : '$' ;
IN    : 'in' ;
SCOPENAME : DOLLAR ~[ ]+ ;
TEXT  : .+? ;