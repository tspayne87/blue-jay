using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BlueJay.UI.Component.Language
{
  public class ExpressionScope
  {
    public object Data { get; private set; }

    public Dictionary<string, IReactiveProperty> Fields { get; private set; }
    public Dictionary<string, MethodInfo> Methods { get; private set; }

    public ExpressionScope(object data, Dictionary<string, IReactiveProperty> fields, Dictionary<string, MethodInfo> methods)
    {
      Data = data;
      Fields = fields;
      Methods = methods;
    }
  }
}
