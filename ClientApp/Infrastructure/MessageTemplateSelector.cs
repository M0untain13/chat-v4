using System;
using ClientCore.Models;

namespace ClientApp.Infrastructure;

public class MessageTemplateSelector : System.Windows.Controls.DataTemplateSelector
{
    public System.Windows.DataTemplate LeftMessageTemplate { get; set; } = new();
    public System.Windows.DataTemplate RightMessageTemplate { get; set; } = new();

    public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
    {
        if (item is not Message message) 
            throw new Exception();
        return message.Name switch
        {
            "Вы" => RightMessageTemplate,
            _ => LeftMessageTemplate
        };
    }
}