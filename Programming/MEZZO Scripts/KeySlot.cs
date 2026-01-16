public class KeySlot : MonoBehaviour
{ 
  public int correctKeyID; 
  public static event System.Action KeyPlacedEvent;  

  public void PlaceKey(GameObject keyObj) 
  { 
    PianoKey key = keyObj.GetComponent<PianoKey>(); 

    if(keyID != null && slot.correctKeyID == key.keyID) 
      {
        keyObject.transform.SetParent(transform);
        keyObject.transform.localPosition = Vector3.zero;
        keyObject.transform.localRotation = Quaternion.identity;

        Rigidbody rb = keyObj.GetComponent<Rigidbody>();
        
        if(rb != null)
           rb.isKinematic = true;

        KeyPlacedEvent?.Invoke();
      } 
  } 
} 
