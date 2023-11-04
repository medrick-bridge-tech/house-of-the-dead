using UnityEngine;

public class OpenDoor : Interactable
{
   private Animator animator;
   [SerializeField] private GameObject combinationCodePaper;
   private BoxCollider collider;
   protected virtual void Awake()
   {
      collider = GetComponent<BoxCollider>();
      onInteraction.AddListener(HandleOpenDoorAnimation);
   }
   protected override void Start()
   {
      base.Start();
      animator = GetComponent<Animator>();
   }

   private void HandleOpenDoorAnimation(Character character)
   {
      animator.SetBool("IsOpen",true);
      combinationCodePaper.GetComponent<InventoryItem>().enabled = true;
      combinationCodePaper.GetComponent<BoxCollider>().enabled = true;
      onInteraction.RemoveListener(HandleOpenDoorAnimation);
      Destroy(this);
   }
}
