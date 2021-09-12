parser grammar ForParser;

options { tokenVocab=ForLexer; }

expr        : name IN expression EOF
            ;
name        : SCOPENAME
            ;
expression  : (TEXT | SCOPENAME)+
            ;
