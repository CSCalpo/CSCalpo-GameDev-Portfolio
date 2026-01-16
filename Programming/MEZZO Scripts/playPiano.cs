public class playPiano : MonoBehaviour //script that prompts player to 'play' piano when puzzle is complete
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

    if(journalObjective != null) 
      { 
        journalObjective.Objective1(); 
      } 
  } 

  void puzzleComplete() 
  { 
      placedKeys++; 

      if(placedKeys >= totalKeys) 
        { 
          playPianoPanel.SetActive(true); 
          Cursor.lockState = CursorLockMode.None; 
          Cursor.visible = true; 
    
          playerMove.canMove = false; 
          Time.timeScale = 0f; 

          if(journalObjective != null) 
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
