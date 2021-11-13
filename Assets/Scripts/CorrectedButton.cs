using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



/**
 * Button that return to normal mode when pointer was pressed and has exited. 
*/
public class CorrectedButton : Button {
	public override void OnPointerExit(PointerEventData eventData) {
		base.OnPointerExit(eventData);

		DoStateTransition(SelectionState.Normal, true);
	}
}
