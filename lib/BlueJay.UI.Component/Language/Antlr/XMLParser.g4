parser grammar XMLParser;

options { tokenVocab=XMLLexer; }

document            : (element | COMMENT)+ EOF
                    ;
slotElement         : OPEN SLOT SLASH_CLOSE
                    ;
element             : OPEN NAME attribute* CLOSE content* OPEN SLASH NAME CLOSE
                    | OPEN NAME attribute* SLASH_CLOSE
                    ;
attribute           : basicAttribute
                    | bindAttribute
                    | eventAttribute
                    ;
basicAttribute      : NAME EQUALS STRING
                    ;
bindAttribute       : COLON NAME EQUALS STRING
                    ;
eventAttribute      : AT NAME (DOT GLOBAL)? EQUALS STRING
                    ;
content             : chardata
                    | slotElement
                    | element
                    | COMMENT
                    ;
chardata            : (TEXT | SEA_WS | EXPRESSION)+ ;