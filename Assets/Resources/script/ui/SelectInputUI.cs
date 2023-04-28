using UnityEngine;
using UnityEngine.UI;

public class SelectInputUI : MonoBehaviour
{

	public InputField nameInputField;
	public Image img;
	private float _a;
	void Start()
	{
		// InputFieldを自動でフォーカスする
		//nameInputField.Select();
		_a = img.color.a;
	}

	public void InputUIImg()
	{
		Color tmpcolor = img.color;
		tmpcolor.a /= 1.25f;
		img.color = tmpcolor;
	}
	public void OutputUIImg()
	{
		Color tmpcolor = img.color;
		tmpcolor.a = _a;
		img.color = tmpcolor;
	}
}