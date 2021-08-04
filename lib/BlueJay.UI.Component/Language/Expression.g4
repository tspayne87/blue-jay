grammar Expression;
prog                  : expr EOF ;
expr                  : basicExpression ;

basicExpression       : literalExpression
                      | functionExpression
                      | scopeExpression
                      | contextVarExpression
                      ;
literalExpression     : STRING
                      | DECIMAL
                      | INTEGER
                      ;
functionExpression    : IDENTIFIER LPARAM argumentExpression RPARAM ;
scopeExpression       : IDENTIFIER ;
contextVarExpression  : DOLLAR E V E N T ;

argumentExpression    : basicExpression (COMMA expr)* ;

// Constants
LPARAM        : '(' ;
RPARAM        : ')' ;
COMMA         : ',' ;
PLUS          : '+' ;
MINUS         : '-' ;
TIMES         : '*' ;
DIVIDE        : '/' ;
DOT           : '.' ;
DOLLAR        : '$' ;
NUMBER        : [0-9] ;
LOWERCASE     : [a-z] ;
UPPERCASE     : [A-Z] ;
ESCSEQ        : '\\' ('b' | 't' | 'n' | 'f' | 'r' | '"' | '\'' | '\\') ;

// Combinations
STRING        : '\'' ( ESCSEQ | ~('\\'|'\'') )* '\''  ;
DECIMAL       : MINUS? NUMBER+ DOT NUMBER ;
INTEGER       : MINUS? NUMBER+ ;
LETTER        : LOWERCASE | UPPERCASE ;
IDENTIFIER    : (LETTER | '_') (LETTER | NUMBER | '_')+  ;


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