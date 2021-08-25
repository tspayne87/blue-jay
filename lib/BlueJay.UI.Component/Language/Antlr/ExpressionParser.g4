parser grammar ExpressionParser;

options { tokenVocab=ExpressionLexer; }

expr                        : expression EOF
                            ;
expression                  : literalExpression
                            | functionExpression
                            | identifier
                            | scopeVarExpression
                            ;
literalExpression           : integer
                            | decimal
                            | boolean
                            | string
                            ;
functionExpression          : identifier LPARAM argumentExpression? RPARAM
                            ;
scopeVarExpression          : DOLLAR SCOPEIDENTIFIER
                            ;
argumentExpression          : expression (COMMA expression)*
                            ;
identifier                  : IDENTIFIER
                            ;
decimal                     : DECIMAL
                            ;
integer                     : INTEGER
                            ;
string                      : STRING
                            ;
boolean                     : TRUE
                            | FALSE
                            ;