## Description
The following are the two command line calls to generate changes that happen in the g4 files, you will
need to be in the Antlr folder to run the commands so that all the files are generated properly.  They will
neeed to be ran one after the other to work together

```wsl
antlr4 -Dlanguage=CSharp -package BlueJay.UI.Component.Generated -o ../Generated JayTMLLexer.g4
antlr4 -Dlanguage=CSharp -visitor -package BlueJay.UI.Component.Generated -o ../Generated JayTMLParser.g4
```
