using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SteppedRangeAttribute))]
public class SteppedRangeDrawer : PropertyDrawer
{
    private float value;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var rangeAttribute = (SteppedRangeAttribute)base.attribute;

        if (property.propertyType == SerializedPropertyType.Float)
        {
            value = EditorGUI.Slider(position, label.text, value, rangeAttribute.min, rangeAttribute.max);

            if (value % rangeAttribute.step != 0)
            {
                float lowerStep = ((int)(value / rangeAttribute.step) * rangeAttribute.step) + rangeAttribute.min;
                float upperStep = (lowerStep + rangeAttribute.step) > rangeAttribute.max ? rangeAttribute.max : lowerStep + rangeAttribute.step;

                bool closerToLower = value - lowerStep < upperStep - value;

                if (closerToLower)
                    value = lowerStep;
                else
                    value = upperStep;
            }

            property.floatValue = value;
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use Range with float or int.");
        }
    }
}
