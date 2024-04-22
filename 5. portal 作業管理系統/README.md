# portal 作業管理系統

## 介紹

這是一個模擬作業管理與提交的系統，可視為簡化版的資料管理系統。

## 功能

測試帳號密碼 **( 環境預設 )**

> 測試帳號

| 使用者名稱 | Teacher A | Teacher C | Student B | Student D | Student E |
| :---- | :---- | :---- | :---- | :---- | :---- |
| 帳號 | aaa | ccc | bbb | ddd | eee |
| 密碼 | 111 | 333 | 222 | 444 | 555 |
| 身份 | 老師 | 老師 | 學生 | 學生 | 學生 |

    本系統包含複數學生，複數老師出不同作業之情境。
    
    但不包含人員增減的功能，因預設背景為選課完成後，不會再修改成員名單。

    師生關聯性，如下所示
    ：Teacher A - Student B
    ：Teacher C - Student B、Student D、Student E

- [x] 帳號功能：登入、修改帳密、登出。
- [x] 管理功能：新增作業、刪除作業、修改作業、成績批改
- [x] 上傳功能：作業上傳、題目上傳 (新增作業)
- [x] 下載功能：作業下載、題目下載

## 檔案夾說明

本專案的檔案儲存路徑：

- 集合所有作業的管理系統資料夾。
    ~~~
    portal 系統 >> portal >> bin >> Debug >> 作業檔案
    ~~~

- 作業題目、所有該作業之學生的繳交檔案資料夾。
    ~~~
    portal 系統 >> portal >> bin >> Debug >> 作業檔案 >> 作業ID-xxxx
    ~~~

- 該作業的某學生的繳交檔案中的其中一版。
    ~~~
    ... >> 作業檔案 >> 作業ID-xxxx >> 學生ID-yyyyyy >> 版本-zzzzz
    ~~~

- 使用 Microsoft Access Database (.accdb) 作為管理後端之資料庫。 
    ~~~
    portal 系統 >> portal >> bin >> Debug >> 作業資料表.accdb
    portal 系統 >> portal >> bin >> Debug >> 導生資料表.accdb
    portal 系統 >> portal >> bin >> Debug >> 帳號密碼資料表.accdb
    portal 系統 >> portal >> bin >> Debug >> 繳交作業資料表.accdb
    ~~~

- 執行檔，可直接使用下列資料夾。
    ~~~
    portal 系統 >> EXE (詳情，請參考【資料庫資料.txt】)
    ~~~

### 產品規格書 ( Spec )

- [OOA](./assets/documents/OOA-第12組.pdf)：物件導向需求討論與分析。
- [OOD](./assets/documents/OOD-第12組.pdf)：物件導向系統設計方法。
- [OOT](./assets/documents/OOT-第12組.pdf)：物件導向測試文件。

> <details>
> 
> <summary>更多軟體工程技術說明</summary>
> 
> >  ## 軟體工程
> > 
> > 軟體工程 ( software engineering )，顧名思義就是「製作軟體的工程方法 ( engineering methodologies )」，用以確保高品質的軟體產品能夠在有限的資源、預算與時間內開發完成。
> > 
> > <details>
> > 
> > <summary>詳細介紹</summary>
> > 
> > <br>
> > 
> > 在軟體從無到有的過程，透過一些工程科學的方法，讓軟體的開發能夠如同其它商品的開發一樣，具有有效的方法來確保軟體開發的效率、品質以及成本的控管。
> > 
> > 提供包含分析、設計、評量、實作、測試、維護與精進的工程方法。這些所謂的工程方法必須是易於理解、管理與應用的方法，同時也被期待是易於再製且能夠具有標準化程序的方法。用更簡單的話來說，軟體工程就是採用系統化、規章化、可量測的方法，來進行軟體的開發與維護。
> > 
> > 通常軟體的開發過程，就是不斷提升抽象化層次的過程，從最高抽象層次的系統分析、設計，到逐漸具體化的程式碼撰寫與測試等過程。我們必須建構出完善的工程原理，來幫助軟體的開發獲致具有經濟效益、且能夠在真實機器上提供穩定與效率的高品質軟體。
> > 
> > 透過一些容易理解的模型，針對不同抽象層次建立並使用這些模型，可在真正開發軟體之前得到可以呈現最終軟體產品的需求與使用者界面模型，有助於軟體開發團隊與使用者進行相關的討論。更具體的設計模型，則可以用來描述實際需要開發的各項軟體組件之功能與設計準則。建置模型則可以應用在軟體的配置與維護等方面的工作。
> 
> > ### 軟體開發模型 ( Software Development Model )
> > 
> > 典型的開發模型，包括：瀑布模型 ( waterfall model )、雛型 / 原型模型 ( prototype model )、漸增模型 ( incremental model )、螺旋模型 ( spiral model )、演化模型 ( evolutionary model ) / 迭代模型 ( Iterative Model )、噴泉模型 ( fountain model )、混合模型 ( hybrid model )。
> > 
> > #### 常見開發模型
> > 
> > <details>
> > 
> > <summary>詳細介紹</summary>
> > 
> > 1. 邊做邊改模型 ( Build-and-Fix Model )
> > 
> >     遺憾的是，許多產品都是使用 “邊做邊改” 模型來開發的。在這種模型中，既沒有規格說明，也沒有經過設計，軟體隨著客戶的需要一次又一次地不斷被修改。
> > 
> >     在這個模型中，開發人員拿到項目立即根據需求編寫程式，調試通過後生成軟體的第一個版本。在提供給用戶使用後，如果程式出現錯誤，或者用戶提出新的要求，開發人員重新修改代碼，直到用戶滿意為止。
> > 
> >     這是一種類似作坊的開發方式，對編寫幾百行的小程式來說還不錯，但這種方法對任何規模的開發來說都是不能令人滿意的，其主要問題在於：
> >     1) 缺少規劃和設計環節，軟體的結構隨著不斷的修改越來越糟，導致無法繼續修改；
> >     2) 忽略需求環節，給軟體開髮帶來很大的風險；
> >     3) 沒有考慮測試和程式的可維護性，也沒有任何文檔，軟體的維護十分困難。
> > 
> > 2. 瀑布模型 ( Waterfall Model )
> > 
> >     瀑布模型，又稱為 Plan-driven model，在開始前計畫所有程序。將軟體生命周期劃分為制定計劃、需求分析、軟體設計、程式編寫、軟體測試和運行維護等六個基本活動，並且規定了它們自上而下、相互銜接的固定次序，如同瀑布流水，逐級下落。
> > 
> >     在瀑布模型中，軟體開發的各項活動嚴格按照線性方式進行，當前活動接受上一項活動的工作結果，實施完成所需的工作內容。當前活動的工作結果需要進行驗證，如果驗證通過，則該結果作為下一項活動的輸入，繼續進行下一項活動，否則返回修改。
> > 
> >     瀑布模型強調文檔的作用，並要求每個階段都要仔細驗證。但是，這種模型的線性過程太理想化，已不再適合現代的軟體開發模式，幾乎被業界拋棄，其主要問題在於：
> >     1) 各個階段的劃分完全固定，階段之間產生大量的文檔，極大地增加了工作量；
> >     2) 由於開發模型是線性的，用戶只有等到整個過程的末期才能見到開發成果，從而增加了開發的風險；
> >     3) 早期的錯誤可能要等到開發後期的測試階段才能發現，進而帶來嚴重的後果。
> > 
> > 3. 快速原型模型 ( Rapid Prototype Model )
> > 
> >     Software Prototype，初步的軟體系統版本 (系統雛型)，協助用戶了解軟體該做些什麼以及如何做。
> >     
> >     Rapid Prototype，部份完成的目標應用程式 (如 GUI 元件)，可透過客戶與開發者確認需求，有效且實際的方式以得知客戶的需求
> > 
> >     快速原型模型的第一步是建造一個快速原型，實現客戶或未來的用戶與系統的交互，用戶或客戶對原型進行評價，進一步細化待開發軟體的需求。通過逐步調整原型使其滿足客戶的要求，開發人員可以確定客戶的真正需求是什麼；第二步則在第一步的基礎上開發客戶滿意的軟體產品。
> > 
> >     顯然，快速原型方法可以剋服瀑布模型的缺點，減少由於軟體需求不明確帶來的開發風險，具有顯著的效果。
> >     
> >     快速原型的關鍵在於儘可能快速地建造出軟體原型，一旦確定了客戶的真正需求，所建造的原型將被丟棄。因此，原型系統的內部結構並不重要，重要的是必須迅速建立原型，隨之迅速修改原型，以反映客戶的需求。
> > 
> > 4. 增量模型 ( Incremental Model )
> > 
> >     軟體產品是被增量式地一塊塊開發。增量模型在各個階段並不交付一個可運行的完整產品，而是交付滿足客戶需求的一個子集的可運行產品。整個產品被分解成若幹個構件，開發人員逐個構件地交付產品，這樣做的好處是軟體開發可以較好地適應變化，客戶可以不斷地看到所開發的軟體，從而降低開發風險。
> > 
> >     但是，增量模型也存在以下缺陷：
> >     1) 由於各個構件是逐漸併入已有的軟體體系結構中的，所以加入構件必須不破壞已構造好的系統部分，這需要軟體具備開放式的體系結構。
> >     2) 在開發過程中，需求的變化是不可避免的。增量模型的靈活性可以使其適應這種變化的能力大大優於瀑布模型和快速原型模型，但也很容易退化為邊做邊改模型，從而是軟體過程的控制失去整體性。
> > 
> >     在使用增量模型時，第一個增量往往是實現基本需求的核心產品。核心產品交付用戶使用後，經過評價形成下一個增量的開發計劃，它包括對核心產品的修改和一些新功能的發佈。這個過程在每個增量發佈後不斷重覆，直到產生最終的完善產品。
> >     
> >     例如，使用增量模型開發字處理軟體。可以考慮，第一個增量發佈基本的文件管理、編輯和文檔生成功能，第二個增量發佈更加完善的編輯和文檔生成功能，第三個增量實現拼寫和文法檢查功能，第四個增量完成高級的頁面佈局功能。
> > 
> > 5. 螺旋模型 ( Spiral Model )
> > 
> >     「螺旋模型」將瀑布模型和快速原型模型結合起來，強調了其他模型所忽視的「風險分析」，特別適合於大型複雜的系統。
> > 
> >     螺旋模型沿著螺線進行若幹次迭代，圖中的四個象限代表了以下活動：
> >     1) 制定計劃：確定軟體目標，選定實施方案，弄清項目開發的限制條件；
> >     2) 風險分析：分析評估所選方案，考慮如何識別和消除風險；
> >     3) 實施工程：實施軟體開發和驗證；
> >     4) 客戶評估：評價開發工作，提出修正建議，制定下一步計劃。
> > 
> >     螺旋模型由風險驅動，強調可選方案和約束條件從而支持軟體的重用，有助於將軟體質量作為特殊目標融入產品開發之中。
> > 
> >     但是，螺旋模型也有一定的限制條件，具體如下：
> >     1) 螺旋模型強調風險分析，但要求許多客戶接受和相信這種分析，並做出相關反應是不容易的，因此，這種模型往往適應於內部的大規模軟體開發。
> >     2) 如果執行風險分析將大大影響項目的利潤，那麼進行風險分析毫無意義，因此，螺旋模型只適合於大規模軟體項目。
> >     3) 軟體開發人員應該擅長尋找可能的風險，準確地分析風險，否則將會帶來更大的風險。
> > 
> >     第一個階段首先是確定該階段的目標，完成這些目標的選擇方案及其約束條件，然後從風險角度分析方案的開發策略，努力排除各種潛在的風險，有時需要通過建造原型來完成。如果某些風險不能排除，該方案立即終止。最後，評價該階段的結果，並設計下一個階段。
> > 
> > 6. 演化模型 ( Evolutionary Model ) / 迭代模型 ( Iterative Model )
> > 
> >     不要求一次性地開發出完整的軟體系統，將軟體開發視為一個逐步獲取需求、完善產品的過程。
> > 
> >     主要針對事先不能完整定義需求的軟體開發。用戶可以給出待開發系統的核心需求，並且當看到核心需求實現後，能夠有效地提出反饋，以支持系統的最終設計和實現。軟體開發人員根據用戶的需求，首先開發核心系統。當該核心系統投入運行後，用戶試用之，完成他們的工作，並提出精化系統、增強系統能力的需求。軟體開發人員根據用戶的反饋，實施開發的迭代過程。第一迭代過程均由需求、設計、編碼、測試、集成等階段組成，為整個系統增加一個可定義的、可管理的子集。
> > 
> >     在開發模式上採取分批迴圈開發的辦法，每迴圈開發一部分的功能，它們成為這個產品的原型的新增功能。於是，設計就不斷地演化出新的系統。 實際上，這個模型可看作是重覆執行的多個 “瀑布模型”。
> > 
> >     “演化模型” 要求開發人員有能力把項目的產品需求分解為不同組，以便分批迴圈開發。這種分組並不是絕對隨意性的，而是要根據功能的重要性及對總體設計的基礎結構的影響而作出判斷。有經驗指出，每個開發迴圈以六周到八周為適當的長度。
> > 
> > 7. 噴泉模型 ( Fountain Model )
> > 
> >     噴泉模型，認為軟體開發過程的各個階段是相互重疊和多次反覆的。就像噴泉一樣，水噴上去又可以落下來，既可以落在中間，又可以落到底部。
> >     
> >     各個開發階段沒有特定的次序要求，完全可以並行進行；可以在某個開發階段中隨時補充其他任何開發階段中遺漏的需求。
> >     
> >     優點：提高開發效率縮短開發週期
> >     缺點：難於管理
> >     
> >     是一種以用戶需求為動力，以物件為驅動的模型，主要用於描述物件導向的軟體開發過程。
> > 
> > 8. 混合模型 ( Hybrid Model )
> > 
> >     過程開發模型 ( Process development model )，又叫混合模型 ( Hybrid model )，或元模型 ( Meta-model )，是把幾種不同模型組合成一種的混合模型，
> > 
> >     它允許一個項目能沿著最有效的路徑發展，這就是過程開發模型(或混合模型)。實際上，一些軟體開發單位都是使用幾種不同的開發方法組成他們自己的混合模型。
> > 
> > </details>
> > 
> > #### 軟體開發模型的比較
> > 
> > <details>
> > 
> > <summary>詳細介紹</summary>
> > 
> > <br>
> > 
> > 每個軟體開發組織應該選擇適合於該組織的軟體開發模型，並且應該隨著當前正在開發的特定產品特性而變化，以減小所選模型的缺點，充分利用其優點。
> > 
> > 下表列出了幾種常見模型的優缺點。
> > 
> > | 模型 | 優點 | 缺點 |
> > | :---- | :---- | :---- |
> > | 瀑布模型 | 文檔驅動 | 系統可能不滿足客戶的需求 |
> > | 快速原型模型 | 關註滿足客戶需求 | 可能導致系統設計差、效率低，難於維護 |
> > | 增量模型 | 開發早期反饋及時，易於維護 | 需要開放式體繫結構，可能會設計差、效率低 |
> > | 螺旋模型 | 風險驅動 | 風險分析人員需要有經驗且經過充分訓練 |
> > 
> > </details>
> 
> > ### 物件導向 ( Object-Oriented )
> > 
> > 物件導向程式設計 ( Object-Oriented Programming ) 是一種程式設計方法論，它將軟體系統中的事物 ( 稱為物件，Object )，視為具有狀態和行為的實體，並將它們組織成一個相互作用的系統 ( 稱為類別，Class )。
> > 
> > <details>
> > 
> > <summary>詳細介紹</summary>
> > 
> > <br>
> > 
> > 類別定義一件事物的抽象特點。類別的定義包含了資料的形式 ( 屬性，Field ) 以及對資料的操作 ( 方法，Method )。我們也可以想像成類別是汽車的設計藍圖 ( blueprint )。
> > 
> > 這種程式設計方法著重於封裝、繼承和多型等概念，使得程式碼可以更加模組化、易於維護和擴展。物件導向程式設計已成為現代軟體開發的主要範式之一，被廣泛應用於許多領域，包括桌面應用程式、網路應用程式、手機應用程式、遊戲開發等。
> > 
> > 物件導向程式設計的三大特性：
> > - 封裝 ( Encapsulation )：將物件的狀態和行為封裝在一個單元中，並且只對外部提供有限的接口來訪問物件，從而保護物件的內部狀態不被直接訪問或修改。
> > - 繼承 ( Inheritance )：通過建立一個新物件，讓其繼承一個或多個已有物件的屬性和方法，從而使得新物件可以重複使用已有物件的程式碼，同時也可以擴展或修改已有物件的功能。
> > - 多型 ( Polymorphism )：讓物件可以根據上下文表現出不同的行為，即一個物件可以被當作多個不同的物件使用，從而提高程式的靈活性和可擴展性。多型通常可以通過多載 ( Overloading ) 和 複寫 ( Overriding ) 來實現。
> > 
> > </details>
> 
> > ### 軟體工程流程
> > 
> > 以物件導向技術進行軟體工程各項活動，包含：OOA、OOD、OOP、OOT。
> > 
> > <details>
> > 
> > <summary>詳細介紹</summary>
> > 
> > - OOA ( Object-oriented analysis，物件導向分析 )
> > 
> >     - 確定需求，業務按照物件導向的思考方式來分析問題。
> >     
> >     - OOA 強調在系統調查資料的基礎之上，也就是對 OO 方法所需要的素材進行的歸類分析和整理。
> > 
> > - OOD ( Object-oriented design，物件導向設計 )
> > 
> >     - 提出方法，包含對管理業務現狀和方法的分析。
> >     
> >     - OOD 涉及「軟體設計方法」和「OOA 的規範化整理」，是銜接 OOA 與 OOP 之間的橋樑。
> > 
> > - OOP ( Object-oriented programming，物件導向程式設計 )
> > 
> >     - 軟體開發，按照規格書與計畫安排，完成實際的開發任務。
> > 
> >     - Common Fundamental Activities (共同的基本活動)：
> >         - Specification (規格)，制定需求文件。
> >         - Development (開發)，達成需求的規格。
> >         - Validation (驗證)，確認是否達到要求。
> >         - Evolution (進化)，隨客戶需求增加新功能。
> > 
> > - OOT ( Object-oriented test，物件導向測試 )
> > 
> >     - 建置與維運，可以應用在軟體的配置與維護等方面的工作。
> > 
> >     - 軟體品質 ( Software Quality )：如何評定軟體的品質是一件相當重要的課題。
> >     
> >         常見的軟體品質評量指標：
> >         - 可用性 ( Usability )：軟體能夠提供使用者所需的服務，或是讓使用者容易地完成工作。
> >         - 效率性 ( Efficiency )：軟體的執行不會浪費如 CPU 或網路頻寬等資源。
> >         - 穩定性 ( Reliability )：軟體可以正確地執行，而不會發生錯誤。
> >         - 可維護性 ( Maintainability )：軟體功能可以容易地被修改。
> >         - 可重用性 ( Reusability )：既有的軟體功能可以套用在其它軟體專案的開發上。
> > 
> > </details>
> 
> > ### 核心技術：UML 表現力
> > 
> > UML ( Unified Modeling Language，統一塑模語言 )，是一種標準化的表示語言，使開發者對軟體系統有具體說明、視覺化、建構與文件化的方法。特別是在軟體開發中，UML 是開發團隊與客戶間，描述工作的重要溝通方式。
> > 
> > <details>
> > 
> > <summary>詳細介紹</summary>
> > 
> > - [UML 設計的主要目標](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#why-uml)
> > 
> > - [UML 概述](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#uml-overview)
> > 
> >     - 結構圖 ( Structure diagrams ) 顯示了系統及其各個部分在不同抽象和實現層級上的靜態結構以及它們之間的相互關係。結構圖中的元素代表了系統有意義的概念，可能包括抽象、現實世界和實現概念，結構圖有以下七種類型：
> > 
> >         - [Class Diagram ( 類別圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#class-diagram)
> >         - [Component Diagram ( 元件圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#component-diagram)
> >         - [Deployment Diagram ( 部署圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#deployment-diagram)
> >         - [Object Diagram ( 物件圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#object-diagram)
> >         - [Package Diagram ( 封裝圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#package-diagram)
> >         - [Composite Structure Diagram ( 複合結構圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#composite-structure-diagram)
> >         - [Profile Diagram ( 剖面圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#profile-diagram)
> > 
> >     - 行為圖 ( Behavior diagrams ) 顯示了系統中物件的動態行為 ( dynamic behavior )，可以描述為系統隨時間的一系列變化 ( a series of changes to the system over time )，行為圖有以下七種類型：
> > 
> >         - [Use Case Diagram ( 用例圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#use-case-diagram)
> >         - [Activity Diagram ( 活動圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#activity-diagram)
> >         - [State Machine Diagram ( 狀態機圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#state-machine-diagram)
> >         - [Sequence Diagram ( 序列圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#sequence-diagram)
> >         - [Communication Diagram ( 通訊圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#communication-diagram)
> >         - [Interaction Overview Diagram ( 互動概覽圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#interaction-overview-diagram)
> >         - [Timing Diagram ( 時序圖 )](https://www.visual-paradigm.com/guide/uml-unified-modeling-language/what-is-uml/#timing-diagram)
> > 
> > - 其他資訊
> >     - [範例說明](https://junwu.nptu.edu.tw/dokuwiki/doku.php?id=se2021:uml)
> >     - [UML 工具](https://www.visual-paradigm.com/tw/features/uml-tool/)
> >     - [其他繪製工具](https://app.diagrams.net/)
> > 
> > </details>
> 
> </details>

## 注意事項

1. 限制與通則：

    1. 下載後檔案會直接存入預設的「本機 >> 下載資料夾」。
    2. 不可直接上傳或下載資料夾，一次僅能上傳一個文件，請壓縮後再上傳。
    3. 上傳檔案的檔案名稱需加上副檔名。
    4. 作業ID會使用隨機亂碼，有極小機率會撞名導至系統出錯。

2. 介面差異：

    - 教師介面，僅顯示每位學生的最後繳交紀錄，並且只會為其評分。
    - 學生介面，可見自己上傳之所有作業紀錄，未受到評分之繳交紀錄成績一律以 -1 呈現。

3. 可能出現的錯誤訊息：

    - ERROR：System.InvalidOperationException: ''Microsoft.ACE.OLEDB.12.0' 提供者並未登錄於本機電腦上。'

        - 發生原因：Server 上沒有安裝 Access Database Engine 的驅動程式。

        - 解決方式：確定執行程式的電腦，是否已安裝 Microsoft Access Database Engine。若無，可參考下列網站。 

            - [找不到 AccessDataSource](https://dotblogs.com.tw/mis2000lab/2013/01/29/accessdatasource_microsoft_ace_oledb)：舊版官方下載連結已失效。
            - 新版官方下載連結：[Microsoft Access Database Engine](https://www.microsoft.com/zh-tw/download/details.aspx?id=54920)

    - 您的應用程式發生未處理的例外狀況："找不到路徑 '作業ID-xxxx >> { 題目檔案 }'的一部分。"

        - 發生原因：教師介面，點選【修改作業】後，未使用【操作完成】按鈕，而是直接關閉「修改作業內容」的視窗。
        
        - 解決方式：該錯誤操作，會導致題目檔案遺失，需要再次進入【修改作業】，使用【上傳題目】，重新上傳題目檔案。

## Demo

1. 請由下列路徑，開啟執行檔 portal.exe。

    > portal 系統 >> EXE (詳情，請參考【資料庫資料.txt】) >> portal.exe

2. 啟動畫面

    - 功能選單【使用者】：登入、修改帳密、登出。
    
        - 請先登入帳號，完成身份確認，才能啟用「修改帳密」的功能。
    
    ![Start](./assets/images/1.%20Start.JPG)

3. 登入 (學生版介面)

    ![Login ( student )](./assets/images/2.%20Login%20(%20student%20).JPG)
    
    - 作業列表 (左半項目)：羅列所有該學生之作業。
    
    - 繳交紀錄 (右半項目)：根據「作業列表」之選項，顯示該項目的繳交狀況。
    
    - 按鈕【作業上傳】：根據「作業列表」之選項，選擇檔案上傳，增加一筆繳交紀錄。
    
    - 按鈕【作業下載】：根據「繳交紀錄」之選項，取得過去繳交的檔案，下載檔案至「本機 >> 下載資料夾」。
    
    - 按鈕【題目下載】：根據「作業列表」之選項，取得該作業題目的檔案，下載檔案至「本機 >> 下載資料夾」。
    
    ![Submit](./assets/images/3.%20Submit.JPG)

4. 登入 (教師版介面)

    ![Login ( teacher )](./assets/images/4.%20Login%20(%20teacher%20).JPG)
    
    - 作業列表 (左半項目)：羅列該老師所出之所有作業。
    
    - 繳交紀錄 (右半項目)：根據「作業列表」之選項，顯示該項目的繳交狀況。
    
    - 按鈕【新增作業】：增加一筆項目至「作業列表」。

    - 按鈕【刪除作業】：根據「作業列表」之選項，移除該作業項目。

    - 按鈕【修改作業】：根據「作業列表」之選項，修改該作業項目的內容。

    - 按鈕【作業下載】：根據「繳交紀錄」之選項，取得學生繳交的檔案，下載檔案至「本機 >> 下載資料夾」。
    
    - 按鈕【題目下載】：根據「作業列表」之選項，取得該作業題目的檔案，下載檔案至「本機 >> 下載資料夾」。
    
    ![Assignment](./assets/images/5.%20Assignment.JPG)

    - 按鈕【成績批改】：根據「繳交紀錄」之選項，給予學生的繳交作業成績評分。
    
    ![Score](./assets/images/6.%20Score.JPG)