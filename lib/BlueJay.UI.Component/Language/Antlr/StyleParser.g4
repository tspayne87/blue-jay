parser grammar StyleParser;

options { tokenVocab=StyleLexer; }

expr                        : style EOF ;
style                       : styleItem (SEMICOLON styleItem)* ;
styleItem                   : WIDTH COLON (integer | expression)
                            | WIDTHPERCENTAGE COLON (decimal | expression)
                            | HEIGHT COLON (integer | expression)
                            | HEIGHTPERCENTAGE COLON (decimal | expression)
                            | TOPOFFSET COLON (integer | expression)
                            | LEFTOFFSET COLON (integer | expression)
                            | PADDING COLON (integer | expression)
                            | HORIZONTALALIGN COLON (horizontalAlign | expression)
                            | VERTICALALIGN COLON (verticalAlign | expression)
                            | POSITION COLON (position | expression)
                            | NINEPATCH COLON (ninePatch | expression)
                            | TEXTCOLOR COLON (color | expression)
                            | BACKGROUNDCOLOR COLON (color | expression)
                            | TEXTALIGN COLON (textAlign | expression)
                            | TEXTBASELINE COLON (textBaseline | expression)
                            | GRIDCOLUMNS COLON (integer | expression)
                            | COLUMNGAP COLON (point | expression)
                            | COLUMNSPAN COLON (integer | expression)
                            | COLUMNOFFSET COLON (integer | expression)
                            | FONT COLON (word | expression)
                            | TEXTUREFONT COLON (word | expression)
                            | TEXTUREFONTSIZE COLON (integer | expression)
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
expression                  : EXPRESSION
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