parser grammar ForParser;

options { tokenVocab=ForLexer; }

expr        : VAR name IN expression EOF
            ;
name        : Text+
            ;
expression  : Text+
            ;
