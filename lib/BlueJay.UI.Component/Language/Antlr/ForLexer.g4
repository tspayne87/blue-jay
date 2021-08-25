lexer grammar ForLexer;

DOLLAR  : '$' ;
VAR   : 'var' ;
IN    : 'in' ;
SCOPENAME : DOLLAR ~[ ]+ ;
TEXT  : .+? ;