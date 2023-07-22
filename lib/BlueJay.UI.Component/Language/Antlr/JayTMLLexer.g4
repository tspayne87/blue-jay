lexer grammar JayTMLLexer;

// --- Basic Processing ---
COMMENT        : '<!--' .*? '-->' ;
OPEN           : '<'                                                             -> pushMode(ATTRIBUTE) ;
SEA_WS         : [ \t\r\n]+                                                      -> skip ;
OPENEXPRESSION : '{{'                                                            -> pushMode(EXPRESSION) ;
TEXT           : ~[<{ \t\r\n]+ ;

// --- Inside Attributes ---
mode ATTRIBUTE;

CLOSE                       : '>'                                                -> popMode ;
SLASH_CLOSE                 : '/>'                                               -> popMode ;
EXPRESSIONATTR              : ':' StartCharacter NameChar* '="'                  -> pushMode(EXPRESSION) ;
EVENTATTR                   : '@' StartCharacter NameChar* ('.' NameChar*)? '="' -> pushMode(EXPRESSION) ;
STYLEATTR                   : 'Style="'                                          -> pushMode(STYLE) ;
STRATTR                     : StartCharacter NameChar* '="'                      -> pushMode(INSIDEATTRIBUTESTRING) ;
IF                          : 'if="'                                             -> pushMode(EXPRESSION) ;
FOR                         : 'for="'                                            -> pushMode(FOREXPRESSION) ;
REF                         : 'ref="'                                            -> pushMode(REFEXPRESSION) ;
SLOT                        : 'Slot' ;
NAME                        : StartCharacter NameChar* ;
SLASH                       : '/' ;
WS                          : [ \t\r\n]+                                         -> skip ;

// --- Fragments Inside Attributes ---
fragment Digit              : [0-9] ;
fragment StartCharacter     : [A-Z] ;
fragment NameChar           : StartCharacter | [a-z] | Digit ;
// --- End Attributes ---

// --- Start Expression ---
mode EXPRESSION;

EXPCLOSE                    : '"'                                                -> popMode ;
CLOSEBRACKETS               : '}}'                                               -> popMode ;
STARTSTRING                 : '\''                                               -> pushMode(INSIDESTRING) ;
EXITINTERP                  : '}'                                                -> popMode ;
AND                         : '&&' ;
OR                          : '||' ;
NOT                         : '!' ;
TRUE                        : 'true' ;
FALSE                       : 'false' ;
GT                          : '>' ;
GTE                         : '>=' ;
LT                          : '<' ;
LTE                         : '<=' ;
EQ                          : '==' ;
COMMA                       : ',' ;
DOT                         : '.' ;
LPAREN                      : '(' ;
RPAREN                      : ')' ;
PLUS                        : '+' ;
MINUS                       : '-' ;
TIMES                       : '*' ;
DIVIDE                      : '/' ;
MOD                         : '%' ;
DOLLAR                      : '$' ;
HASH                        : '#' ;
OPENSQUAREBRACKET           : '[' ;
CLOSESQUAREBRACKET          : ']' ;
NUMERIC                     : '-'? [0-9]+ ('.' [0-9]+)? ;
IDENTIFIER                  : [a-zA-Z_] [a-zA-Z_0-9]* ;
EXPRWS                      : [ \r\t\u000C\n]+                                   -> skip;
// --- End Expression ---

// --- Inside String ---
mode INSIDESTRING;

CLOSESTRING                 : '\''                                               -> popMode ;
CLOSEINTERP                 : '{'                                                -> pushMode(EXPRESSION) ;
ESCAPESTRING                : '\\\''
                            | '\\\\'
                            | '\\}'
                            | '\\{'
                            ;
DETAILSSTRING               : ~['\\{}]+ ;
// --- End String ---

// --- Inside Attribute String ---
mode INSIDEATTRIBUTESTRING;

ATTRCLOSESTRING             : '"'                                               -> popMode ;
ATTRDETAILSSTRING           : ~["]+ ;
// --- End Attribute String ---

// --- Start For Expression ---
mode FOREXPRESSION;

FORCLOSE                    : '"'                                                -> popMode ;
FOROPENEXPRESSION           : '{{'                                               -> pushMode(EXPRESSION) ;
FORIN                       : 'in' ;
FORDOUBLEDOT                : '..' ;
FORHASH                     : '#' ;
FORINTEGER                  : ForDash? ForInteger+ ;
FORIDENTIFIER               : ForNameChar ForRefChar* ;

// --- Fragments ---
fragment ForRefChar         : ForNameChar | ForInteger ;
fragment ForNameChar        : [A-Z] | [a-z] ;
fragment ForInteger         : [0-9] ;
fragment ForDash            : '-' ;
// --- End For Expression ---

// --- Start Ref Expression ---
mode REFEXPRESSION;

REFCLOSE                    : '"'                                                -> popMode ;
REFNAME                     : RefStartCharacter RefNameChar* ;

// --- Fragments ---
fragment RefDigit           : [0-9] ;
fragment RefStartCharacter  : [A-Z] ;
fragment RefNameChar        : RefStartCharacter | [a-z] | RefDigit ;
// --- End Ref Expression

// --- Start Style Expression ---
mode STYLE;

STYLECLOSE                  : '"'                                                -> popMode ;
STYLEOPENEXPRESSION         : '{{'                                               -> pushMode(EXPRESSION) ;
STYLEDOUBLECOLON            : '::' ;
STYLECOLON                  : ':' ;
SEMICOLON                   : ';' ;
STYLECOMMA                  : ',' ;
INTEGER                     : Minus? StyleDigit+ ;
DECIMAL                     : Minus? StyleDigit+ Dot StyleDigit+ ;
STYLEWS                     : [ \t\r\n]+ -> skip ;

// --- Alternate Styles ---
HOVER                       : 'Hover' ;

// --- Final Word to deal with strings ---
WORD                         : (StyleNameChar | Underscore | BackSlash)+ ;

// --- Fragments ---
fragment Minus              : '-' ;
fragment Dot                : '.' ;
fragment Underscore         : '_' ;
fragment BackSlash          : '/' ;
fragment StyleDigit         : [0-9] ;
fragment Upper              : [A-Z] ;
fragment Lower              : [a-z] ;
fragment StyleNameChar      : Upper | Lower | StyleDigit ;

// --- End Style Expression ---