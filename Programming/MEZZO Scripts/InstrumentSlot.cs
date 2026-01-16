public class InstrumentSlot : MonoBehaviour
{ 
  public int InstrumentID; 
  public static event System.Action InstrumentPlacedEvent;  

  public void PlaceInstrument(GameObject InstruObj) 
  { 
      InstrumentID instrumentID = InstruObj.GetComponent<InstrumentID>(); 

      if(instrumentID != null && instrumentID.instruID == InstrumentID) 
        { 
          InstruObj.transform.SetParent(transform); 
          InstruObj.transform.localPosition = Vector3.zero; 
          InstruObj.transform.localRotation = Quaternion.identity; 
          InstruObj.GetComponent<Rigidbody>().isKinematic = true; 

          if(InstrumentPlacedEvent != null) 
            { 
            InstrumentPlacedEvent.Invoke(); 
            } 
        } 
  } 
}

