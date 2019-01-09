using System;
using System.Windows.Markup;

namespace NorthwindInventory.Helpers
{
	/// <summary>
	/// Mainly used for the simplicity of the code.
	/// The value converters can now be used in XAML bindings like this
	/// {Binding X, Converter={x:MyConverter}}.
	/// </summary>
	public abstract class BaseConverter : MarkupExtension
	{
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
