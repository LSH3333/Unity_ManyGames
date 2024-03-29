﻿using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System;
using System.Collections.Generic;
using System.Collections;

public class AdminManager : MonoBehaviour
{
    public Text privateDataTxt;

    public Text No, UserName, UserPw, CreateDate;

    private int selectNum;
    // User 제거 클릭 시 확인하는 박스 
    public GameObject areYouSureBoxObj;
    public InputField deleteUserIF;

    public void Awake()
    {
        InitAdminBoard();
    }

    // get user private data  
    private void InitAdminBoard()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserPrivateData");
        // 계정 생성 순 정렬 
        query.AddDescendingOrder("AccoutCreateDate");

        // Board Init 전에 초기화 
        No.text = "";
        UserName.text = "";
        UserPw.text = "";
        CreateDate.text = "";

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("NCMB Get Private Data Failed" + e.ErrorMessage);
            }
            else
            {                                
                string res = "";
                privateDataTxt.text = "";
                int rank = 0;
                int cnt = 0; // 10위 까지만 보여줌

                foreach (NCMBObject obj in objList)
                {
                    No.text += string.Format("{0:D2}. ", (++rank)) + "\n";
                    UserName.text += obj["UserName"] + "\n";
                    UserPw.text += obj["UserPw"] + "\n";
                    CreateDate.text += obj["AccountCreateDate"] + "\n";                    

                    cnt++;
                    if (cnt >= 10) break;
                }

            }
        });
    }

    private void DeleteNCMBObject()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserPrivateData");
        // 계정 생성 순 정렬 
        query.AddDescendingOrder("AccoutCreateDate");
        
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("NCMB Get Private Data Failed" + e.ErrorMessage);
            }
            else // get data success 
            {
                int cnt = 1;
                foreach (NCMBObject obj in objList)
                {
                    if (cnt == selectNum)
                    {
                        Debug.Log("DeleteAsync " + selectNum);

                        // 해당 유저로 로그인 
                        NCMBUser.LogInAsync(obj["UserName"].ToString(), obj["UserPw"].ToString(), (NCMBException exx) =>
                        {
                            if (exx != null) { Debug.Log("Delete User, Login Failed"); }
                            else // 해당 유저로 로그인 성공 
                            {
                                Debug.Log("Delete User, Login Success: " + NCMBUser.CurrentUser.UserName);

                                // 로그인한 유저 계정 데이터 삭제 (UserManageMent)
                                NCMBUser.CurrentUser.DeleteAsync((NCMBException de) =>
                                {
                                    if(de != null) { Debug.Log("UserManageMent Delete Fail"); }
                                    else
                                    {
                                        Debug.Log("UserManageMent Delete Success");

                                        // 해당 유저 Datastore UserPrivateData 테이블에서 해당 유저 행 삭제 
                                        obj.DeleteAsync((NCMBException ex) =>
                                        {
                                            if (ex != null) { Debug.Log("NCMB Delete Async Failed"); }
                                            else
                                            {
                                                Debug.Log("NCMB Delete Async Success");

                                                // Admin으로 다시 로그인 
                                                NCMBUser.LogInAsync("Admin", "Admin", (NCMBException e_admin) =>
                                                {
                                                    if (e_admin != null) { Debug.Log("Admin re-login Failed"); }
                                                    else // Admin re login 성공  
                                                    {
                                                        Debug.Log("Admin re-login Success: " + NCMBUser.CurrentUser.UserName);

                                                        // 유저 삭제 -> Admin re-login -> 적용된 보드 다시 랜더링
                                                        InitAdminBoard();
                                                    }

                                                });

                                                
                                            }
                                        });
                                        
                                    }
                                });
                            }

                        });
                        
                    }

                    cnt++;
                }

            }
        });
    }

    //// 삭제할 유저 선택할때 해당 유저로 로그인해야 하는데 이 과정에서 Admin 계정이 자동 로그아웃.
    //// 따라서 다시 Admin 계정으로 로그인해줄 필요가 있다. 
    //private void LogInAsAdmin()
    //{
    //    // 해당 유저로 로그인 
    //    NCMBUser.LogInAsync("Admin", "Admin", (NCMBException exx) =>
    //    {
    //        if (exx != null) { Debug.Log("Admin re-login Failed"); }
    //        else // 해당 유저로 로그인 성공 
    //        {
    //            Debug.Log("Admin re-login Success: " + NCMBUser.CurrentUser.UserName);
    //        }

    //    });
    //}

    public void OnClickBack()
    {
        GetComponent<Animator>().SetTrigger("popout");
        Destroy(gameObject, 1f);
    }

    public void OnClickSelect(int _selectNum)
    {
        selectNum = _selectNum;
        ActivateAreYouSureBox();
        // 해당 NCMB 오브젝트 지우고 
        //DeleteNCMBObject();
    }

    public void ActivateAreYouSureBox()
    {
        areYouSureBoxObj.SetActive(true);
    }

    public void OnClickRemove()
    {
        if (deleteUserIF.text == "delete")
        {
            // 해당 NCMB 오브젝트 지우고 
            DeleteNCMBObject();
            areYouSureBoxObj.GetComponent<Animator>().SetTrigger("popout");
            deleteUserIF.text = "";
            StartCoroutine(UnactiveBox());
        }
        else
        {            
        }
    }

    private IEnumerator UnactiveBox()
    {
        yield return new WaitForSeconds(1f);
        areYouSureBoxObj.SetActive(false);
        yield return null;
    }
}
