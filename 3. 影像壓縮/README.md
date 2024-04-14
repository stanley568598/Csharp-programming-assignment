# 影像壓縮

## 介紹

影像壓縮是資料壓縮技術在數位影像上的應用，目的是減少圖像資料中的冗餘資訊，從而用更加高效的格式儲存和傳輸資料。

<details>

<summary>更多壓縮技術的說明</summary>

### 壓縮原理

壓縮的通則是利用資料的一致性，越一致的資料，越能夠進行壓縮。

- 資料越一致，代表統計特性越集中，包括傅立葉轉換域 ( Fourier transform domain )、直方圖 ( histogram )、特徵值 ( eigenvalue ) ...... 等方面的集中度。
    
    - 空間上的一致性：影像中每一點的值，會和相鄰的點的值非常接近。

    - 頻率上的一致性：一張影像的頻譜大多都集中在低頻的地方。

        > 【影像的「頻率」是指在空間域 (space domain) 進行分析】
        > - 低頻成分：代表變化較為緩和的地方。
        > 
        >   - 對應的是影像的「顏色」(color) 和「強度」(intensity)。
        > 
        > - 高頻成分：代表變化較為劇烈的地方。
        > 
        >   - 對應的是影像的「邊緣」(edge) 和「雜訊」(noise)。
        > 

除此之外，也可利用資料的規則性與可預測性來對其作壓縮。

### 常見的影像壓縮技術

壓縮的技術主要分成兩種：

1. 失真壓縮 ( lossy compression )：壓縮率較高，但無法重建原來的資料。例如：

    - 色彩深度 (8bit、10bit)
    - 色彩取樣 (4:2:2、4:2:0)
    - DFT ( Discrete Fourier Transform，離散傅立葉變換 )
    - DCT ( Discrete Cosine Transform，離散餘弦變換 )
    - 多項式曲線的近似 ( polynomial approximation )

2. 無損壓縮 ( lossless compression )：壓縮率較低，但可以重建原來的資料。例如：
   
   - 二元編碼 ( binary coding )
   - 霍夫曼編碼 ( Huffman coding )
   - 算術編碼 ( arithmetic coding )。 

#### <font color="red"> 色彩深度 (索引色) </font>

- 色彩深度，簡稱色深。

    - 在電腦圖學領域中，表示在點陣圖緩衝區中儲存顏色所用的位元數。

- 色彩深度越高，可用的顏色就越多。

    - 色彩深度是用「n 位元顏色」（n-bit colour）來說明的。
    
    - 若色彩深度是 n 位元，即有 $`2^{n}`$ 種顏色選擇，而儲存每像素所用的位元數目就是 n。
        
        - 例如：8 位元 = 256 種顏色；10 位元 = 1024 種顏色。 

        - 其中，每像素所用 8 位元 (0-255) 來表示 256 種的顏色之一。

- 這種做法又被稱為「索引色 ( Indexed color )」或 「調色盤」。

    - 在計算機領域當中，索引色是一種資料壓縮的技巧，主要是用來快速呈現圖片或加速資料傳輸，也稱之為「向量量化壓縮」。
    
        1. 在一張圖片中選擇最常見的顏色，製成顏色表，一同儲存在圖片中。
        
            - 點陣圖緩衝區。
        
        2. 使用歐幾里德距離公式，對一張全彩的圖片進行有限顏色的置換。
            
            - 換言之，該張圖片並不包含原圖的所有顏色。

            - 顏色資訊也不會直接存在該張圖片的像素裡，而是給予索引來參照其中的「調色盤」。

    - 通常使用 256 種索引色彩，即 8-bit ( 256 色 ) 壓縮出來的圖片，看起來跟真彩色差不多，檔案大小則變得很小。

#### 色彩取樣 ( 4：2：2 或 4：2：0 )

- 人類的視覺系統，對於亮度比較敏感，而對於彩度比較不敏感。

    - 因此我們可以利用人類視覺的特性，減少 Cb、Cr 的取樣個數，同時保持足夠的視覺質量。

    - 像素 ( pixel ) 的 RGB 值，可利用以下的公式轉換成 YCbCr。

        > Y = 0.299 R + 0.578 G + 0.114 B
        > 
        > Cb = -0.169 R - 0.331 G + 0.500 B；( Cb = 0.565 (B - Y) )
        > 
        > Cr = 0.500 R - 0.419 G - 0.081 B；( Cr = 0.713 (R - Y) )

        其中 Y 是亮度 ( Luminance )，C 是色差 ( chrominance )，Cb 為藍色色差，Cr 為紅色色差。

- 此技術運用的是空間上的一致性，取樣格式有 4：2：2 與 4：2：0 兩種。

    - 未壓縮 4:4:4
    
        - 當所有像素的 CbCr 資訊都被記錄下來時（無未採樣的像素），色度採樣即可被標示為 4:4:4。
        
        - 可提供最高品質的影像，RAW 檔案也等同 4:4:4。
        
        - 然而，檔案大小也最大。

    - 運用子採樣，損耗一些顏色資訊，來壓縮檔案。( 即不記錄某些像素的資訊。)
    
        - YCbCr 4:2:2 子採樣：在每個像素行中，只有兩個像素的 CbCr 訊號被記錄下來。 
            <table style="border-color:black; border-collapse: collapse; border: solid 1px;">
                <tr>
            　  <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
                <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
                <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
                <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
                </tr>
                <tr>
            　  <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
            <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
            <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
            <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
                </tr>
            </table>
            <table style="border-color:black; border-collapse: collapse; border: solid 1px;">
                <tr>
            　  <td style="width:30px; height:30px; background-color:rgba(255,0,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;">  </td>
                <td style="width:30px; height:30px; background-color:rgba(0,100,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;">  </td>
                </tr>
                <tr>
            　  <td style="width:30px; height:30px; background-color:rgba(0,255,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;">  </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;">  </td>
                </tr>
            </table>
            <div style="width:230px; font-size: 30px; vertical-align:middle; text-align:center;">&darr;</div>
            <table style="border-color:black; border-collapse: collapse; border: solid 1px;">
                <tr>
            　  <td style="width:30px; height:30px; background-color:rgba(255,0,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(255,0,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Copy </td>
                <td style="width:30px; height:30px; background-color:rgba(0,100,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(0,100,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Copy </td>
                </tr>
                <tr>
            　  <td style="width:30px; height:30px; background-color:rgba(0,255,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(0,255,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Copy </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Copy </td>
                </tr>
            </table>
        - YCbCr 4:2:0 子採樣：在每兩個像素行中，上面一行只記錄兩個像素的 CbCr 訊號。下面一行的像素則完全不記錄 CbCr 訊號。
            <table style="border-color:black; border-collapse: collapse; border: solid 1px;">
                <tr>
            　  <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
                <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
                <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
                <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
                </tr>
                <tr>
            　  <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
            <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
            <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
            <td style="width:30px; height:30px; background-color:silver; vertical-align:middle; text-align:center;"> Y </td>
                </tr>
            </table>
            <table style="border-color:black; border-collapse: collapse; border: solid 1px;">
                <tr>
            　  <td style="width:30px; height:30px; background-color:rgba(255,0,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;">  </td>
                <td style="width:30px; height:30px; background-color:rgba(0,100,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;">  </td>
                </tr>
                <tr>
            　  <td style="width:30px; height:30px; background-color:rgba(255,255,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;">  </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;">  </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;">  </td>
                <td style="width:30px; height:30px; background-color:rgba(255,255,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;">  </td>
                </tr>
            </table>
            <div style="width:230px; font-size: 30px; vertical-align:middle; text-align:center;">&darr;</div>
            <table style="border-color:black; border-collapse: collapse; border: solid 1px;">
                <tr>
            　  <td style="width:30px; height:30px; background-color:rgba(255,0,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(255,0,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Copy </td>
                <td style="width:30px; height:30px; background-color:rgba(0,100,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Cr<br>Cb </td>
                <td style="width:30px; height:30px; background-color:rgba(0,100,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Copy </td>
                </tr>
                <tr>
                <td style="width:30px; height:30px; background-color:rgba(255,0,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Copy </td>
                <td style="width:30px; height:30px; background-color:rgba(255,0,0,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Copy </td>
                <td style="width:30px; height:30px; background-color:rgba(0,100,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Copy </td>
                <td style="width:30px; height:30px; background-color:rgba(0,100,255,0.5); font-size: 70%; vertical-align:middle; text-align:center;"> Copy </td>
                </tr>
            </table>

#### DFT ( Discrete Fourier Transform，離散傅立葉變換 )

- DFT (離散傅立葉變換)，是一種頻域表示方法，廣泛使用於有限長序列的在數位訊號處理。
   
    - Fourier Series 傅立葉級數：將一個週期信號分解成無限多分開的離散弦波。

        > 度量觀點轉換：
        > 
        > 1. 世界上所有的訊號，都是由不同頻率的弦波所組成。
        > 
        > 2. 弦波是訊號的基本組成元素，不管它是正弦或餘弦。
        > 
        > 3. 從不同的面相 ( 時域 / 頻域 ) 去分析訊號，可以獲得不同的視圖 ( 波型 / 頻譜 )。
 
    -  Fourier Transform 傅立葉轉換：用來將時域訊號，轉換成頻域訊號的計算方式。

#### <font color="red"> DCT ( Discrete Cosine Transform ) 離散餘弦變換 </font>

- DCT (離散餘弦變換)，是在 DFT 的基礎上推導出來的，是 DFT 的特殊形式。

    - 在 FT (傅立葉轉換) 的算法中包含複數運算，其運算複雜度和存儲所需的空間都超過實數運算。
    
    - 為了簡化計算過程，離散餘弦轉換（Discrete Cosine Transform），改以實部運算代替 FT 中的虛部運算，藉此達到簡化目的。
    
        - 當是實函數時，DFT 的實部是偶函數，虛部是奇函數。
        - 當是偶實函數時，DFT 的虛部就是 0。

    - DCT 會將任何一個輸入訊號先拓展為一個實偶訊號，藉此形成簡化運算。
    
        - 在 DFT 傅立葉級數展開式中，如果被展開的函數是實偶函數，那麼其傅立葉級數就只包含餘弦項，再將其離散化 (DFT) 可導出該餘弦項的餘弦變換，就是離散餘弦變換 (DCT)。

- <font color="red"> 8×8 離散餘弦轉換 (DCT) </font>

    - 對於人眼而言，影像的低頻部分資訊量是大於高頻部分的。
    
        - 因此刪除高頻資訊，對於人眼視覺所能辨別的資訊量來說，損失是非常少的，但可大幅減少資料傳輸量。
    
    - 此技術運用的是頻率上的一致性。
    
        - 降低記憶體的需要量
        - 降低運算量

    - 通常我們會將影像切成 8×8 的方格，才進行離散餘弦轉換 (DCT)。
        
        - 原因如下：一張影像的每個區塊，其高低頻成分都不一樣。若對整張影像直接作離散餘弦轉換 (DCT)，多少會有高頻成分的出現。如果切成 8×8 的方格，則對大部分的方格幾乎都沒有高頻成分。
    
    - 經過離散餘弦轉換 (DCT) 後的 8×8 矩陣稱為 DCT 矩陣。

        - DCT 矩陣最左上角的係數稱為直流 (DC) 成分，而其他 63 個係數則稱為交流 (AC) 成分。

        - 越靠近左上角的係數，表示頻率較低的部分；而越往右下角方向的係數，代表的頻率則越高。
    
        - 2D 的 8×8 DCT 的輸出通常會按照 "zigzag" 的順序，將 2D 轉為 1D 的型態。
        
            - 按照此順序排列，能量可能較大的會被擺在前面，而後面的高頻成分從某個值開始後幾乎為零，以符號 EOB (end of block) 表示，指後面的高頻的部分經過量化 (quantization) 之後皆為 0。

- JPEG 壓縮

    - JPEG（Joint Photographic Experts Group）是一種常用的圖像壓縮格式，用於將圖像文件縮小並減少其占用的存儲空間。
    
        1. 主要使用了離散余弦轉換（DCT）技術，將圖像的像素數據轉換為一系列餘弦波的系數，以表示圖像中的不同頻率部分。
        
        2. 將這些餘弦波的頻率從低到高排列，並且對應不同級別的細節和色彩變化。
        
            - 低頻信息包含了圖像的整體結構和一般特徵。

            - 高頻信息包含了圖像中的細節和細微變化。
            
        3. DCT 係數的值表示了每個餘弦波在圖像中的貢獻程度。

            - 低頻部分的餘弦波具有較大的係數值。

            - 高頻部分的餘弦波具有較小的係數值。

        4. 量化的目的是通過去除高頻信息來減小文件的大小，同時盡量保留圖像的視覺質量。

            - 在量化過程中，我們使用一個量化表將 DCT 係數進行除法運算，然後四捨五入到最近的整數。
            
            - 這樣，高頻的係數值通常會被縮小到接近零的程度，並且可能被設置為零。(高頻的係數通常都遠小於低頻的係數)
            
                - 較小的量化表將導致更少的信息丟失和較好的圖像質量，但也將使文件大小增加。
            
                - 較大的量化表將導致更多的信息丟失和較差的圖像質量，但也將使文件大小減小。
        
        5. Zig-Zag (曲折掃描)

            - JPEG 影像壓縮技術之所以使用曲折掃描的原因是，它能合理的期待在一個區塊中的像素相較於在一條直線上的像素的出現頻率會比較高，而出現頻率高就代表能壓縮的比例高。
            
                - 例如，某個區塊中的顏色都是不同深淺的藍色，只需要儲存該區塊內不同像素的差異性而不必將區塊內所有像素的一切資訊都儲存，而更進一步的，這些差異值有可能會是 0，或是一個非常小而可以視為 0 的差異。這樣的技術允許圖像或是影片能有更佳的壓縮率。

</details>

## 功能

- [x] 輸入點陣圖 (.bmp) 影像檔
- [x] 輸出壓縮圖片

> (256色) 調色盤方案 與 (16x16) DCT 方案
    
    - 調色盤方案：採用的是空間上一致性的壓縮做法。
    - DCT 方案：採用的是頻率上一致性的壓縮做法。
    
    詳細技術介紹，請參考 "更多壓縮技術的說明"！

> 測試圖片

    t.bmp：表示輸入的原始圖片 (test)
    r.bmp：表示輸出的壓縮圖片 (result)

&#8251; 點陣圖 (.bmp)：不含任何處理的圖片儲存格式，直接儲存每一個像素的圖片資訊。

## Demo

### 1. 調色盤

- 操作流程：

    1. Click 左上角功能區【File >> Open】，可以將原始圖片輸入視窗 (如視窗左圖)。 
    
    2. Click 【256調色盤】的按鈕，開始顏色置換，產生輸出資料：
        
        - 壓縮圖片 (如視窗右圖)。
        - 顯示調色盤的顏色種類 (右下文字框)。
        - PSNR 值 (左下數值)。

    3. Click 左上角功能區【File >> Save】，可以將壓縮圖片存入本機。
    
    ![256 Color Palette](./assets/images/1.%20256%20Color%20Palette.JPG)

- 踩坑紀錄：

    1. 需自行設定【索引色儲存格式】，否則依然會按照正常 bmp 儲存方式，將所有像素的顏色資訊都儲存下來。

        - Issue：原始圖片的儲存格式，未提供調色盤設定，直接置換完 256 色，會發現檔案大小沒有變化，表示索引功能未正常運作。
        
        - Solve：建立新的 Bitmap，宣告儲存格式為 PixelFormat.Format8bppIndexed。此時才存在 bmp.Palette 可供 ColorPalette 物件操作，進行顏色的設置與讀取。
        
            - PixelFormat.Format8bppIndexed，表示設定色彩深度為 8 位元的索引表儲存格式。(必需完成此設定，置換成 256 顏色後，才能正常使用調色盤的 Index，儲存成壓縮檔案。)
    
    2. 專有名詞解釋：

        - PSNR (Peak signal-to-noise ratio，正常訊號與雜訊的峰值比)：表示訊號最大可能功率和影響它表示精度的破壞性雜訊功率的比值。

            - 在影像壓縮時，經過壓縮與解壓縮重建的影像 與 原始影像的差距是通常使用 MSE (mean-square error，平均平方誤差，均方差) 來衡量。

                $`MSE = \frac{1}{mn} \sum\limits^{m-1}_{i=0} \sum\limits^{n-1}_{j=0} [ I(i,j)- K(i,j) ]^{2}`$

                - 圖像大小為 m × n
                - I 代表無雜訊的原始圖像(未壓縮)
                - K 為 I 的雜訊近似圖像(K 為 I 經過壓縮後的圖像)
            
            - 則 PSNR 定義為：

                $`PSNR = 10 \cdot \log_{10} (\frac{MAX_{I}^{2}}{MSE}) = 20 \cdot \log_{10} (\frac{MAX_{I}}{\sqrt{MSE}})`$

                - 其中，$`MAX_{I}`$ 是表示圖像點顏色的最大數值，如果每個採樣點用 8 位元表示，那麼就是 255。
            
            - PSNR 峰值訊噪比，經常用作為影像處理中的圖像壓縮等領域中訊號重建品質的量測方法。
                
                - 圖像與影像壓縮中典型的峰值訊噪比值在 30 dB 到 50 dB 之間，愈高愈好。
                
                - PSNR 接近 50 dB，代表壓縮後的圖像僅有些許非常小的誤差。
                
                - PSNR 大於 30 dB ，人眼很難察覺壓縮後和原始影像的差異。
                
                - PSNR 介於 20 dB 到 30 dB 之間，人眼就可以察覺出圖像的差異。
                
                - PSNR 介於 10 dB 到 20 dB 之間，人眼還是可以用肉眼看出這個圖像原始的結構，且直觀上會判斷兩張圖像不存在很大的差異。
                
                - PSNR 低於 10 dB，兩圖像看起來完全不同。

- 評估結果：

    - t.bmp (原始圖像) vs r.bmp (壓縮圖像)
    
        - 位元深度：32 bit vs 8 bit
        - 檔案大小：263 KB vs 64.9 KB
        - PSNR：15.45 dB
        - 評語：大量減少檔案大小，但壓縮圖片與原圖存在不小的差異。
    
        ![Compare File Size-1](./assets/images/2.%20Compare%20File%20Size.JPG)

### 2. 離散餘弦轉換 (DCT)

- 操作流程：

    1. Click 左上角功能區【File >> Open】，可以將原始圖片輸入視窗 (如視窗左圖)；Select【壓縮因子】，調整壓縮效果。 

        - 壓縮因子：探討放大量化表，對壓縮效果的改變。
        
            ( 實際量化表 = 預設量化表 * 壓縮因子 )
    
    2. Click 【DCT】的按鈕，經過 DCT、量化 (quantize)、曲折掃描 (Zig-zag) 封裝，產生輸出資料：
        
        - 統計報表 (report.txt)：儲存紀錄壓縮所省略的資訊量。
        - 壓縮資料 (result.txt)：Zig-zag 將 2D 的圖像資料，轉換成 1D 的儲存形式，形成格式的空間壓縮。
        - 統計壓縮所省略的資訊量 (左下數值)。

    3. Click 【IDCT】的按鈕，讀取 result.txt，經過 逆 Zig-zag、逆量化、IDCT 還原圖像，產生輸出資料：

        - 壓縮圖片 (如視窗右圖)。
        - PSNR 值 (右下數值)。

    4. Click 左上角功能區【File >> Save】，可以將壓縮圖片存入本機。

    ![16x16 DCT](./assets/images/3.%2016x16%20DCT.JPG)

- 踩坑紀錄：
    
    1. 過程詳解： 

        1. DCT $`\to`$ 量化 (quantize) $`\to`$ 曲折掃描 (Zig-zag)。
            - DCT：將圖片轉換成頻域資料。
            - 量化：濾頻，去除高頻資料。
            - 曲折掃描：頻域資料的儲存方式，具備空間一致性，可以較小的方式儲存檔案。
        
        2. 逆曲折掃描 $`\to`$ 逆量化 $`\to`$ IDCT。
            - 逆曲折掃描：讀取 Zig-zag 格式所儲存的資料。
            - 逆量化：高頻資料大多都轉換成 0，低頻資料皆以較低的數值儲存，需升高回原本的值域。
            - IDCT (inverse DCT)：將頻域資料轉換回時域資料，將壓縮資料重建成有損圖片。

    2. 專有名詞解釋：

        > 對於下列公式的參數假設：
        > - 影像大小 = $`N \times N`$。
        > - $`f(x, y)`$ 為座標 $`(x, y)`$ 的單一通道值。 
        > - $`D(i,j)`$ 是 頻率域 ( Frequency domain ) 上影像 $`(i,j)`$ 位置的頻率係數值。
        > - Coefficient (C)：
        > 
        >     $`C(i) = \begin{cases} \frac{1}{\sqrt{2}} & \text{if i=0} \\ 1  & \text{otherwise}\end{cases} , C(j) = \begin{cases} \frac{1}{\sqrt{2}} & \text{if j=0} \\ 1  & \text{otherwise}\end{cases}`$

        1. DCT：將圖片資料轉換成頻域資料，達成濾頻的目的。
            
            $`D(i, j) = \frac{1}{2\sqrt{2N}} C(i) C(j) \sum\limits^{N-1}_{x=0} \sum\limits^{N-1}_{y=0} f(x,y) cos(\frac{(2x+1) i \pi}{2N}) cos(\frac{(2y+1) j \pi}{2N})`$

            , $`0 \leq i , j \leq (N-1)`$.

        2. 反函數 IDCT (inverse DCT)：將頻域資料轉回時域資料，將壓縮資料重建成壓縮圖片。

            $`f(x,y) = \frac{1}{2\sqrt{2N}} \sum\limits^{N-1}_{i=0} \sum\limits^{N-1}_{j=0} C(i) C(j) D(i,j) cos(\frac{(2x+1) i \pi}{2N}) cos(\frac{(2y+1) j \pi}{2N})`$
            , $`0 \leq x , y \leq (N-1)`$.

- 評估結果：

    1. 不同壓縮因子，對壓縮影像的影響：

        ![Compress Factor=1](./assets/images/4.%20Compress%20Factor=1.JPG)

        ![Compress Factor=2](./assets/images/5.%20Compress%20Factor=2.JPG)

        ![Compress Factor=4](./assets/images/6.%20Compress%20Factor=4.JPG)

    2. 統計壓縮所省略的資訊量 & 壓縮效果的 PSNR 值

        ![Compare with Compress Factors](./assets/images/7.%20Compare%20with%20Compress%20Factors.JPG)

    3. 比較輸出檔案

        ![Compare File Size-2](./assets/images/8.%20Compare%20File%20Size.JPG)

        | 項目 | 原始圖像 | 壓縮因子=1 | 壓縮因子=2 | 壓縮因子=4 |
        | :---- | :---- | :---- | :---- | :---- |
        | 檔案大小 | 263 KB (t.bmp) | 40 KB (result1.txt)<hr>192 KB (r1.bmp) | 23 KB (result2.txt)<hr>192 KB (r2.bmp) | 13 KB (result4.txt)<hr>192 KB (r4.bmp) |
        | PSNR | - | 28.99 dB | 27.21 dB | 25.45 dB |

    4. 評語：

        - 原始影像 (t.bmp) 與 壓縮圖像 (r.bmp) 的檔案大小差異，實際上來源於 32位元 與 24位元 的圖檔格式。
            
            - 皆儲存相同影像大小的全部像素，兩者並無檔案大小的壓縮。
            - DCT 壓縮能力主要體現於 壓縮檔案 (result.txt) 的頻域資料儲存形式上。

        - 提高壓縮因子，會提升量化效果，過濾掉更多高頻資料。
            
            - 壓縮檔案 (result.txt) 效果顯著提升。
            - 圖像重建品質 (PSNR) 微量下降。
            - 壓縮圖像 (r.bmp)，人眼可識別壓縮後和原始影像的差異。
