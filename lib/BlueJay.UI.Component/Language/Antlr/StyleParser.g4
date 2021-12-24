parser grammar StyleParser;

options { tokenVocab=StyleLexer; }

expr                        : style EOF ;
style                       : styleItem (SEMICOLON styleItem)* ;
styleItem                   : WIDTH COLON integer
                            | WIDTHPERCENTAGE COLON decimal
                            | HEIGHT COLON integer
                            | HEIGHTPERCENTAGE COLON decimal
                            | TOPOFFSET COLON integer
                            | LEFTOFFSET COLON integer
                            | PADDING COLON integer
                            | HORIZONTALALIGN COLON horizontalAlign
                            | VERTICALALIGN COLON verticalAlign
                            | POSITION COLON position
                            | NINEPATCH COLON ninePatch
                            | TEXTCOLOR COLON color
                            | BACKGROUNDCOLOR COLON color
                            | TEXTALIGN COLON textAlign
                            | TEXTBASELINE COLON textBaseline
                            | GRIDCOLUMNS COLON integer
                            | COLUMNGAP COLON point
                            | COLUMNSPAN COLON integer
                            | COLUMNOFFSET COLON integer
                            | FONT COLON word
                            | TEXTUREFONT COLON word
                            | TEXTUREFONTSIZE COLON integer
                            | HEIGHTTEMPLATE COLON heightTemplate
                            ;
decimal                     : DECIMAL
                            | INTEGER
                            ;
integer                     : INTEGER
                            ;
ninePatch                   : WORD
                            ;
color                       : integer COMMA integer COMMA integer COMMA (integer COMMA)?
                            ;
point                       : integer COMMA (integer COMMA)?
                            ;
word                        : WORD
                            ;

// --- Enumerations ---
horizontalAlign             : RIGHT
                            | CENTER
                            | LEFT
                            ;
verticalAlign               : TOP
                            | CENTER
                            | BOTTOM
                            ;
textAlign                   : RIGHT
                            | CENTER
                            | LEFT
                            ;
textBaseline                : TOP
                            | CENTER
                            | BOTTOM
                            ;
position                    : RELATIVE
                            | ABSOLUTE
                            ;
heightTemplate              : HEIGHTTEMPLATE
                            ;