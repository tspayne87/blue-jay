﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.UI.Component.Attributes
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  public class InjectAttribute : Attribute
  {
  }
}
