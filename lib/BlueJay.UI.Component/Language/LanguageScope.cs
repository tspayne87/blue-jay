using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BlueJay.UI.Component.Language
{
  public class LanguageScope
  {
    public readonly object Instance;
    public readonly Dictionary<string, object> Props;
    public readonly Dictionary<string, (MethodInfo, List<object>)> Events;

    public LanguageScope Parent { get; set; }
    public List<LanguageScope> Children { get; set; }
    public Dictionary<string, LanguageScope> Slots { get; set; }

    public LanguageScope(object instance)
    {
      Instance = instance;
      Props = new Dictionary<string, object>();
      Events = new Dictionary<string, (MethodInfo, List<object>)>();
    }
  }
}
