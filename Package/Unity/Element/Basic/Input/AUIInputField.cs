using ProceduralLevel.Common.Event;
using TMPro;
using UnityEngine;

namespace ProceduralLevel.UI.Unity
{
	public abstract class AUIInputField<TValue> : ActiveUIElement
	{
		protected TValue m_Value;

		[SerializeField]
		protected TMP_InputField m_InputField = null;

		public TValue Value
		{
			get => m_Value;
			set => SetValue(value);
		}

		public readonly CustomEvent<TValue> OnChanged = new CustomEvent<TValue>();

		protected abstract string ValueToString(TValue value);
		protected abstract TValue StringToValue(string raw);

		#region Element
		protected override void OnInitialize(EventBinder binder)
		{
			base.OnInitialize(binder);

			binder.Bind(OnActive, OnActiveHandler);
			m_Value = StringToValue(m_InputField.text);

			m_InputField.onValueChanged.AddListener(OnInputFieldValueChangedHandler);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			OnChanged.RemoveAllListeners();
		}
		#endregion


		private void SetRawValue(string rawValue)
		{
			SetValue(StringToValue(rawValue));
		}

		public void SetValue(TValue value)
		{
			if(!Equals(m_Value, value))
			{
				m_Value = value;
				m_InputField.text = ValueToString(m_Value);
				OnChanged.Invoke(m_Value);
			}
		}

		#region Callbacks
		private void OnActiveHandler(bool selected)
		{
			if(selected)
			{
				m_InputField.Select();
			}
		}

		private void OnInputFieldValueChangedHandler(string newValue)
		{
			SetRawValue(newValue);
		}
		#endregion
	}
}
