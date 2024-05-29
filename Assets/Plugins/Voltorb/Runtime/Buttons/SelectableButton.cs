using UnityEngine;
using UnityEngine.UI;

namespace Voltorb
{
	public abstract class SelectableButton : Selectable
	{
		
	}

	public abstract class SelectableButton<T> : Selectable
	{
		public abstract void BindProperties(T props);
		public abstract void BindPropertiesToNull();
	}
}
