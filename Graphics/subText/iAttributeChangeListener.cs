using System;

namespace Graphics {
	/// <summary>
	/// Represents an instance that should fire an event when one of its attributes changes its value.
	/// </summary>
	public interface IAttributeChangeListener {
		/// <summary>
		/// Occurs when an attribute's value changes.
		/// </summary>
		event EventHandler AttributeChange;
	}
}

