lexer grammar StyleLexer;


COLON                       : ':' ;
SEMICOLON                   : ';' ;
COMMA                       : ',' ;
INTEGER                     : Minus? Digit+ ;
DECIMAL                     : Minus? Digit+ Dot Digit+ ;
EXPRESSION                  : '{{' ~[}]+ '}}' ;
WS                          : [ \t\r\n]+ -> skip ;

// --- Field Names ---
WIDTH                       : 'Width' ;
WIDTHPERCENTAGE             : 'WidthPercentage' ;
HEIGHT                      : 'Height' ;
HEIGHTPERCENTAGE            : 'HeightPercentage' ;
TOPOFFSET                   : 'TopOffset';
LEFTOFFSET                  : 'LeftOffset' ;
PADDING                     : 'Padding';
HORIZONTALALIGN             : 'HorizontalAlign' ;
VERTICALALIGN               : 'VerticalAlign' ;
POSITION                    : 'Position' ;
NINEPATCH                   : 'NinePatch' ;
TEXTCOLOR                   : 'TextColor' ;
BACKGROUNDCOLOR             : 'BackgroundColor' ;
TEXTALIGN                   : 'TextAlign' ;
TEXTBASELINE                : 'TextBaseline' ;
GRIDCOLUMNS                 : 'GridColumns' ;
COLUMNGAP                   : 'ColumnGap' ;
COLUMNSPAN                  : 'ColumnSpan' ;
COLUMNOFFSET                : 'ColumnOffset' ;
FONT                        : 'Font' ;
TEXTUREFONT                 : 'TextureFont' ;
TEXTUREFONTSIZE             : 'TextureFontSize' ;

// --- Enumerations ---
RIGHT                       : 'Right' ;
CENTER                      : 'Center' ;
LEFT                        : 'Left' ;
TOP                         : 'Top' ;
BOTTOM                      : 'Bottom' ;
RELATIVE                    : 'Relative' ;
ABSOLUTE                    : 'Absolute' ;

// --- Final Word to deal with strings ---
WORD                         : (NameChar | Underscore)+ ;

// --- Fragments ---
fragment Minus              : '-' ;
fragment Dot                : '.' ;
fragment Underscore         : '_' ;
fragment Digit              : [0-9] ;
fragment Upper              : [A-Z] ;
fragment Lower              : [a-z] ;
fragment NameChar           : Upper | Lower | Digit ;