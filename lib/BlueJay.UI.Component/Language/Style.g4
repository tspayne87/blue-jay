grammar Style;
prog                     : expr EOF ;
expr                     : styleExpression
                         ;

styleExpression          : styleItemExpression (SEMICOLON styleExpression)* ;
styleItemExpression      : WIDTH COLON (INTEGER | PERCENTAGE | EXPRESSION)
                         | HEIGHT COLON (INTEGER | PERCENTAGE | EXPRESSION)
                         | TOPOFFSET COLON (INTEGER | EXPRESSION)
                         | LEFTOFFSET COLON (INTEGER | EXPRESSION)
                         | PADDING COLON (INTEGER | EXPRESSION)
                         | HORIZONTALALIGN COLON (RIGHT | CENTER | LEFT | EXPRESSION)
                         | VERTICALALIGN COLON (TOP | CENTER | BOTTOM | EXPRESSION)
                         | POSITION COLON (RELATIVE | ABSOLUTE | EXPRESSION)
                         | NINEPATCH COLON (WORD | EXPRESSION)
                         | TEXTCOLOR COLON (COLOR | EXPRESSION)
                         | BACKGROUNDCOLOR COLON (COLOR | EXPRESSION)
                         | TEXTALIGN COLON (RIGHT | CENTER | LEFT | EXPRESSION)
                         | TEXTBASELINE COLON (TOP | CENTER | BOTTOM | EXPRESSION)
                         | GRIDCOLUMNS COLON (INTEGER | EXPRESSION)
                         | COLUMNGAP COLON (POINT | EXPRESSION)
                         | COLUMNSPAN COLON (INTEGER | EXPRESSION)
                         | COLUMNOFFSET COLON (INTEGER | EXPRESSION)
                         | FONT COLON (WORD | EXPRESSION)
                         | TEXTUREFONT COLON (WORD | EXPRESSION)
                         | TEXTUREFONTSIZE COLON (INTEGER | EXPRESSION)
                         ;

// Combinations
POINT         : INTEGER (COMMA INTEGER)? ;
COLOR         : INTEGER COMMA INTEGER COMMA INTEGER (COMMA INTEGER)? ;
WORD          : LETTER (LETTER | NUMBER)+ ;
EXPRESSION    : LCURLYBRACKET ('}' ~'}' | ~'}' .) RCURLYBRACKET ;
PERCENTAGE    : INTEGER PERCENT ;
INTEGER       : MINUS? NUMBER+ ;
DECIMAL       : MINUS? NUMBER+ DOT NUMBER ;
LETTER        : LOWERCASE | UPPERCASE ;

// Enum Fields
RIGHT         : R I G H T ;
CENTER        : C E N T E R ;
LEFT          : L E F T ;
TOP           : T O P ;
BOTTOM        : B O T T O M ;
RELATIVE      : R E L A T I V E ;
ABSOLUTE      : A B S O L U T E ;

// Style Fields
WIDTH           : W I D T H ;
HEIGHT          : H E I G H T ;
TOPOFFSET       : T O P O F F S E T ;
LEFTOFFSET      : L E F T O F F S E T ;
PADDING         : P A D D I N G ;
HORIZONTALALIGN : H O R I Z O N T A L A L I G N ;
VERTICALALIGN   : V E R T I C A L A L I G N ;
POSITION        : P O S I T I O N ;
NINEPATCH       : N I N E P A T C H ;
TEXTCOLOR       : T E X T C O L O R ;
BACKGROUNDCOLOR : B A C K G R O U N D C O L O R ;
TEXTALIGN       : T E X T A L I G N ;
TEXTBASELINE    : T E X T B A S E L I N E ;
GRIDCOLUMNS     : G R I D C O L U M N S ;
COLUMNGAP       : C O L U M N G A P ;
COLUMNSPAN      : C O L U M N S P A N ;
COLUMNOFFSET    : C O L U M N O F F S E T ;
FONT            : F O N T ;
TEXTUREFONT     : T E X T U R E F O N T ;
TEXTUREFONTSIZE : T E X T U R E F O N T S I Z E ;

// Constants
COLON         : ':' ;
SEMICOLON     : ';' ;
MINUS         : '-' ;
DOT           : '.' ;
COMMA         : ',' ;
PERCENT       : '%' ;
LCURLYBRACKET : '{{' ;
RCURLYBRACKET : '}}' ;
NUMBER        : [0-9] ;
LOWERCASE     : [a-z] ;
UPPERCASE     : [A-Z] ;

// Upper/Lower fragments
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