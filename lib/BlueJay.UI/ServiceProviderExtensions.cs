using BlueJay.Common.Systems;
using BlueJay.Core.Container;
using BlueJay.UI.Interfaces;
using BlueJay.UI.Systems;
using BlueJay.UI.Tree;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.UI;

public static class ServiceProviderExtensions
{
  public static void AddBlueJayUISystems(this IServiceProvider provider)
  {
    provider.AddSystem<ViewportSystem>();
  }

  public static void AddBlueJayUIRenderSystems(this IServiceProvider provider)
  {
    provider.AddSystem<NinePatchRenderSystem>();
    provider.AddSystem<TextureRenderSystem>();
    provider.AddSystem<TextRenderSystem>();
  }

  public static IElement CreateUINode(this IServiceProvider provider, string tagName, Style? style = null, IElement? parent = null)
  {
    var element = ActivatorUtilities.CreateInstance<Element>(provider, tagName);
    element.Style = style ?? new Style();
    parent?.AddChild(element);
    return element;
  }

  public static ITextElement CreateUITextNode(this IServiceProvider provider, string text, Style? style = null, IElement? parent = null)
  {
    var element = ActivatorUtilities.CreateInstance<TextElement>(provider, text);
    element.Style = style ?? new Style();
    parent?.AddChild(element);
    return element;
  }

  public static ITextureElement CreateUITextureNode(this IServiceProvider provider, ITexture2DContainer texture, Style? style = null, IElement? parent = null)
  {
    var element = ActivatorUtilities.CreateInstance<TextureElement>(provider, texture);
    element.Style = style ?? new Style();
    parent?.AddChild(element);
    return element;
  }

  public static string GetUIDebugStructureString(this IServiceProvider provider)
  {
    return string.Empty;
  }
}
