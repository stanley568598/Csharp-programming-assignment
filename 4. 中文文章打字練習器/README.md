# 中文文章打字練習器

## 介紹

這是一個簡單的打字練習器，可以自由添加喜歡的文章，調整字型大小設定，自動比對輸入結果。

目標客群：家長、低年級小朋友。

## 功能

- [x] 自由增加、調整題目文章。
- [x] 鎖定答案區複製貼上功能。
- [x] 紀錄答案與比對輸入結果。

## 資料說明

- Article.accdb：紀錄題目與答案的資料庫。

    > 使用 Microsoft Access Database (.accdb) 做為後端儲存庫。 

## 注意事項

可能出現的錯誤訊息：
    
> ERROR：System.InvalidOperationException: ''Microsoft.ACE.OLEDB.12.0' 提供者並未登錄於本機電腦上。'

- 發生原因：Server 上沒有安裝 Access Database Engine 的驅動程式。

- 解決方式：確定執行程式的電腦，是否已安裝 Microsoft Access Database Engine。若無，可參考下列網站。 

    - [找不到 AccessDataSource](https://dotblogs.com.tw/mis2000lab/2013/01/29/accessdatasource_microsoft_ace_oledb)：舊版官方下載連結已失效。
    - 新版官方下載連結：[Microsoft Access Database Engine](https://www.microsoft.com/zh-tw/download/details.aspx?id=54920)

## Demo

1. 介面說明：
    
    - 功能選單【管理者】：增加題目、修改題目、輸入完成。
    - 功能選單【工具】：調整字型大小、插入特殊符號。

    ![Start](./assets/images/1.%20Start.JPG)

2. 讀取資料：

    - 下拉選單【題目】：選擇題號，讀取題目資料。
    - 按鍵【開啟舊紀錄】：讀取對應題目的答題記錄。

    ![Open Record](./assets/images/2.%20Open%20Record.JPG)

3. 儲存與提交：

    - 按鍵【儲存 + 校對】：提交輸入資料進行比對，儲存提交紀錄至後端資料庫。
    
    ![Save & Submit](./assets/images/3.%20Save%20&%20Submit.JPG)
