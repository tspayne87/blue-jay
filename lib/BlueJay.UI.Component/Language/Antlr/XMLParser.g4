parser grammar XMLParser;

options { tokenVocab=XMLLexer; }

document            : (element | COMMENT)+ EOF
                    ;
slotElement         : OPEN SLOT SLASH_CLOSE
                    ;
element             : OPEN NAME attribute* CLOSE content* OPEN SLASH NAME CLOSE
                    | OPEN NAME attribute* SLASH_CLOSE
                    ;
attribute           : ifAttribute
                    | forAttribute
                    | basicAttribute
                    | bindAttribute
                    | eventAttribute
                    | GLOBAL
                    | refAttribute
                    ;
ifAttribute         : COLON IF EQUALS STRING
                    ;
forAttribute        : COLON FOR EQUALS STRING
                    ;
refAttribute        : COLON REF EQUALS STRING
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