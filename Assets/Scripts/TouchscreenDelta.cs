using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.InputSystem.Layouts;



namespace UnityEngine.InputSystem.OnScreen {

	public class TouchscreenDelta :
		OnScreenControl,
		IDragHandler,
		IPointerEnterHandler,
		IPointerExitHandler {

		[InputControl(layout = "Vector2")]
		[SerializeField]
		string m_ControlPath;

		Vector2 m_PointerDownPos;
		bool pointerExited = true;

		protected override string controlPathInternal {
			get => m_ControlPath;
			set => m_ControlPath = value;
		}


		//public void OnPointerDown(PointerEventData eventData) {
		//	if (eventData == null)
		//		throw new System.ArgumentNullException(nameof(eventData));

		//	RectTransformUtility.ScreenPointToLocalPointInRectangle(
		//		transform.parent.GetComponentInParent<RectTransform>(),
		//		eventData.position, eventData.pressEventCamera,
		//		out m_PointerDownPos
		//	);

		//	pointerExited = false;
		//}


		public void OnPointerEnter(PointerEventData eventData) {
			if (eventData == null)
				throw new System.ArgumentNullException(nameof(eventData));

			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				transform.parent.GetComponentInParent<RectTransform>(),
				eventData.position, eventData.pressEventCamera,
				out m_PointerDownPos
			);

			pointerExited = false;
		}


		public void OnPointerExit(PointerEventData eventData) {
			if (eventData == null)
				throw new System.ArgumentNullException(nameof(eventData));

			pointerExited = true;
		}


		public void OnDrag(PointerEventData eventData) {
			if (eventData == null)
				throw new System.ArgumentNullException(nameof(eventData));

			if (!pointerExited) {
				RectTransformUtility.ScreenPointToLocalPointInRectangle(
					transform.parent.GetComponentInParent<RectTransform>(),
					eventData.position, eventData.pressEventCamera,
					out var position
				);

				var delta = position - m_PointerDownPos;
				m_PointerDownPos = position;

				SendValueToControl(delta);
			}
		}
	}
}