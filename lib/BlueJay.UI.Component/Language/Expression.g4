grammar Expression;
prog                     : expr EOF ;
expr                     : basicExpression
                         | ternaryExpression
                         | foreachExpression
                         ;

basicExpression          : literalExpression
                         | functionExpression
                         | scopeExpression
                         | contextVarExpression
                         ;
literalExpression        : STRING
                         | DECIMAL
                         | INTEGER
                         | TRUE
                         | FALSE
                         ;
functionExpression       : IDENTIFIER LPARAM argumentExpression RPARAM ;
scopeExpression          : IDENTIFIER ;
contextVarExpression     : DOLLAR EVENT ;

argumentExpression       : basicExpression (COMMA expr)* ;
ternaryExpression        : basicExpression QUESTION basicExpression COLON basicExpression ;

foreachExpression        : VAR IDENTIFIER IN IDENTIFIER ;


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
QUESTION      : '?' ;
COLON         : ':' ;
SEMICOLON     : ';' ;
NUMBER        : [0-9] ;
LOWERCASE     : [a-z] ;
UPPERCASE     : [A-Z] ;
ESCSEQ        : '\\' ('b' | 't' | 'n' | 'f' | 'r' | '"' | '\'' | '\\') ;

// Combinations
TRUE          : T R U E ;
FALSE         : F A L S E ;
EVENT         : E V E N T ;
STYLE         : S T Y L E ;
STRING        : '\'' ( ESCSEQ | ~('\\'|'\'') )* '\''  ;
DECIMAL       : MINUS? NUMBER+ DOT NUMBER ;
INTEGER       : MINUS? NUMBER+ ;
LETTER        : LOWERCASE | UPPERCASE ;
IDENTIFIER    : (LETTER | '_') (LETTER | NUMBER | '_')+  ;
IN            : I N ;
VAR           : V A R ;


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