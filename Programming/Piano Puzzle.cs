public class PianoPuzzle: MonoBehaviour { 
  private GameObject heldKey = null; 
  private Rigidbody heldKeyRb; 
  private Camera playerCamera; 
  private Quaternion heldKeyOriginalRotation; 
  
  private void Start() 
  { 
    playerCamera = Camera.main; 
  } 
  
  private void Update() 
  { 
    if (Input.GetKeyDown(KeyCode.F)) 
    { 
      
      if 
        (heldKey == null) 
      { 
        HoldingKey(); 
      } 
      
     else 
     {
       
       if 
         (!PlacingKey()) 
       { 
         DropKey(); 
       } 
     } 
    } 
  } 
  
  public void HoldingKey() 
  { 
    RaycastHit hit; 
    
    if 
      (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 8f)) 
    { 
      if (hit.collider.CompareTag("PianoKey")) 
      { 
        heldKey = hit.collider.gameObject; 
        heldKeyRb = heldKey.GetComponent<Rigidbody>();
        
        heldKeyRb.isKinematic = true; 
        
        heldKey.transform.SetParent(playerCamera.transform);
        
        heldKey.transform.localPosition = new Vector3(0, -0.5f, 1); 

        heldKeyOriginalScale = heldKey.transform.localScale;
      } 
    } 
  } 
  
  public bool PlacingKey() 
  { RaycastHit hit; 
   
   if 
     (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 3f)) 
   { 
     if (hit.collider.CompareTag("KeySlot")) 
     { 
       KeySlot slot = hit.collider.GetComponent<KeySlot>(); 
       PianoKey key = heldKey.GetComponent<PianoKey>(); 
       
       if (slot.correctKeyID == key.keyID) 
       { slot.PlaceKey(heldKey); 
        heldKey.transform.SetParent(slot.transform, true); 
        
        heldKey.transform.position = slot.transform.position; 
        heldKey.transform.rotation = slot.transform.rotation; 

        heldKey.transform.localScale = heldKeyOriginalScale;
        
        heldKey.GetComponent<Rigidbody>().isKinematic = true; 
        
        heldKey = null; 
        heldKeyRb = null; 
        return true; 
       } 
     } 
   } 
   return false; 
  } 
  
  public void DropKey() 
  { 
    if (heldKey != null) 
    { 
      heldKey.transform.SetParent(null); 
      
      if (heldKeyRb != null) 
      { 
        heldKeyRb.isKinematic = false; 
      } 
  
      heldKey = null; 
      heldKeyRb = null; 
    } 
  } 
} //end piano piece object

public class KeySlot : MonoBehaviour //SCRIPT TO BE ADDED TO KEYSLOTS
{ 
  public int correctKeyID; 
  public static event System.Action KeyPlacedEvent;  

public void PlaceKey(GameObject keyObj) 
{ 
  PianoKey key = keyObj.GetComponent<PianoKey>(); 

if (keyID != null && slot.correctKeyID == key.keyID) 
{
      keyObject.transform.SetParent(transform);
      keyObject.transform.localPosition = Vector3.zero;
      keyObject.transform.localRotation = Quaternion.identity;

      Rigidbody rb = keyObj.GetComponent<Rigidbody>();
      if (rb != null)
          rb.isKinematic = true;

        KeyPlacedEvent?.Invoke();
      } 
    } 
  } 
}

public class KeyID : MonoBehaviour //to be placed on piano piece
{ 
  public int keyID; 
}

public class playPiano : MonoBehaviour //sample script that prompts player to 'play' piano when puzzle is complete
{ 
  public int totalKeys = 2; 
  private int placedKeys = 0; 
  public GameObject playPianoPanel; 
  public FPSController playerMove; 
  public JournalObjectives journalObjective; 

void Start() 
{ 
  KeySlot.KeyPlacedEvent += puzzleComplete; 
  playPianoPanel.SetActive(false); 

if (journalObjective != null) 
  { 
  journalObjective.Objective1(); 
  } 
} 

void puzzleComplete() 
  { 
  placedKeys++; 

if (placedKeys >= totalKeys) 
  { 
  playPianoPanel.SetActive(true); 
  Cursor.lockState = CursorLockMode.None; 
  Cursor.visible = true; 
  playerMove.canMove = false; 
  Time.timeScale = 0f; 

if (journalObjective != null) 
    { 
    journalObjective.Objective2(); 
    } 
  } 
} 

void OnDestroy() 
{ 
  KeySlot.KeyPlacedEvent -= puzzleComplete; 
} 

public void playCassiopea() 
  { 
  Time.timeScale = 1.0f; SceneManager.LoadSceneAsync(2); 
  } 
}

