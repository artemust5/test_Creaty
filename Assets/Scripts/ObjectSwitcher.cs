using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSwitch;
    private int currentIndex = 0;

    private void Start()
    {
        SwitchObject(currentIndex);
    }

    public void NextObject()
    {
        currentIndex = (currentIndex + 1) % objectsToSwitch.Length;
        SwitchObject(currentIndex);
    }

    public void PreviousObject()
    {
        currentIndex = (currentIndex - 1 + objectsToSwitch.Length) % objectsToSwitch.Length;
        SwitchObject(currentIndex);
    }

    private void SwitchObject(int index)
    {
        foreach (var obj in objectsToSwitch)
        {
            obj.SetActive(false);
        }
        objectsToSwitch[index].SetActive(true);
    }
}
