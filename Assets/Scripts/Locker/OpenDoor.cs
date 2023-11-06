using UnityEngine;

public class OpenDoor : Interactable
{
   private Animator animator;
   [SerializeField] private GameObject insideItem;
   private AudioSource audioSource;
   protected virtual void Awake()
   {
      onInteraction.AddListener(HandleOpenDoorAnimation);
   }
   protected override void Start()
   {
      base.Start();
      animator = GetComponent<Animator>();
      audioSource = GetComponent<AudioSource>();
   }

   private void HandleOpenDoorAnimation(Character character)
   {
      animator.SetBool("IsOpen",true);
      if (insideItem != null)
      {
         insideItem.GetComponent<InventoryItem>().enabled = true;
         insideItem.GetComponent<BoxCollider>().enabled = true;
      }
      onInteraction.RemoveListener(HandleOpenDoorAnimation);
      Destroy(this);
   }
}
