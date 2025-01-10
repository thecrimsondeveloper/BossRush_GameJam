using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private int startingPage;
    [SerializeField] private List<MenuPage> menuPages;



    public void Awake()
    {

        for(int i = 0; i < menuPages.Count; i++)
        {
            if(i == startingPage)
            {
                menuPages[i].Show();
            }
            else
            {
                menuPages[i].Close();
            }
        }
    }

    public void ShowPage(int index)
    {
        for(int i = 0; i < menuPages.Count; i++)
        {
            if(i == index)
            {
                menuPages[i].Show();
            }
            else
            {
                menuPages[i].Close();
            }
        }
    }

    public void ShowNextPage()
    {
        for(int i = 0; i < menuPages.Count; i++)
        {
            if(menuPages[i].gameObject.activeSelf)
            {
                menuPages[i].Close();
                if(i == menuPages.Count - 1)
                {
                    menuPages[0].Show();
                }
                else
                {
                    menuPages[i + 1].Show();
                }
                return;
            }
        }
    }

    public void ShowPreviousPage()
    {
        for(int i = 0; i < menuPages.Count; i++)
        {
            if(menuPages[i].gameObject.activeSelf)
            {
                menuPages[i].Close();
                if(i == 0)
                {
                    menuPages[menuPages.Count - 1].Show();
                }
                else
                {
                    menuPages[i - 1].Show();
                }
                return;
            }
        }
    }

    public void ShowFirstPage()
    {
        for(int i = 0; i < menuPages.Count; i++)
        {
            if(i == 0)
            {
                menuPages[i].Show();
            }
            else
            {
                menuPages[i].Close();
            }
        }
    }



}
