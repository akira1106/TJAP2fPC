﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Drums背景 : CActivity
    {
        // 本家っぽい背景を表示させるメソッド。
        //
        // 拡張性とかないんで。はい、ヨロシクゥ!
        //
        public CAct演奏Drums背景()
        {
            base.b活性化してない = true;
        }

        public void tFadeIn()
        {
            this.ct上背景FIFOタイマー = new CCounter( 0, 100, 6, CDTXMania.Timer );
            this.eFadeMode = EFIFOモード.フェードイン;
        }

        public void tFadeOut()
        {
            this.ct上背景FIFOタイマー = new CCounter( 0, 100, 6, CDTXMania.Timer );
            this.eFadeMode = EFIFOモード.フェードアウト;
        }

        public override void On活性化()
        {
            //Upper_BG内のフォルダ一覧を生成する
            string[] strUpperBG = Directory.GetDirectories( CSkin.Path( "Graphics\\Upper_BG" ) );

            //ランダムで選ぶ
            Random rand = new Random();

            //設定ファイルがあれば読み込む
            base.On活性化();
        }

        public override void On非活性化()
        {
            if( !this.b活性化してない )
            {
                CDTXMania.t安全にDisposeする( ref this.ct上背景FIFOタイマー );
                CDTXMania.t安全にDisposeする( ref this.ct上背景スクロール用タイマー );
                CDTXMania.t安全にDisposeする( ref this.ct下背景スクロール用タイマー1 );

                base.On非活性化();
            }
        }

        public override void OnManagedリソースの作成()
        {
            if( !this.b活性化してない )
            {
                this.tx上背景メイン = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Upper_BG\01\bg.png" ) );
                this.tx上背景クリアメイン = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Upper_BG\01\bg_clear.png" ) );
                this.tx下背景メイン = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Dancer_BG\01\bg.png" ) );
                this.tx下背景クリアメイン = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Dancer_BG\01\bg_clear.png" ) );
                this.tx下背景クリアサブ1 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\Dancer_BG\01\bg_clear_01.png" ) );
                this.ct上背景スクロール用タイマー = new CCounter( 1, 328, 40, CDTXMania.Timer );
                this.ct下背景スクロール用タイマー1 = new CCounter( 1, 1257, 6, CDTXMania.Timer );
                this.ct上背景FIFOタイマー = new CCounter();
                base.OnManagedリソースの作成();
            }
        }

        public override void OnManagedリソースの解放()
        {
            if( !this.b活性化してない )
            {
                CDTXMania.tテクスチャの解放( ref this.tx上背景メイン );
                CDTXMania.tテクスチャの解放( ref this.tx上背景クリアメイン );
                CDTXMania.tテクスチャの解放( ref this.tx下背景メイン );
                CDTXMania.tテクスチャの解放( ref this.tx下背景クリアメイン );
                CDTXMania.tテクスチャの解放( ref this.tx下背景クリアサブ1 );
                base.OnManagedリソースの解放();
            }
        }

        public override int On進行描画()
        {
            if( !this.b活性化してない )
            {
                if( this.b初めての進行描画 )
                {
                    this.b初めての進行描画 = false;
                }

                int[] nBgY = new int[] { 0, 536 };
                this.ct上背景FIFOタイマー.t進行();
                this.ct上背景スクロール用タイマー.t進行Loop();
                this.ct下背景スクロール用タイマー1.t進行Loop();


                for( int i = 0; i < CDTXMania.ConfigIni.nPlayerCount; i++ )
                {
                    if( CDTXMania.ConfigIni.nPlayerCount == 1 && i == 1 ) continue;
                
                    if( this.tx上背景メイン != null )
                    {
                        int nループ幅 = 328;
                        for( int j = 0; j < 5; j++ )
                        {
                            this.tx上背景メイン.t2D描画( CDTXMania.app.Device, ( i * nループ幅 ) - this.ct上背景スクロール用タイマー.n現在の値, nBgY[ i ] );
                        }
                        //this.tx上背景メイン.t2D描画( CDTXMania.app.Device, 0 - this.ct上背景スクロール用タイマー.n現在の値, nBgY[ i ] );
                        //this.tx上背景メイン.t2D描画( CDTXMania.app.Device, ( 1 * nループ幅 ) - this.ct上背景スクロール用タイマー.n現在の値, nBgY[ i ] );
                        //this.tx上背景メイン.t2D描画( CDTXMania.app.Device, ( 2 * nループ幅 ) - this.ct上背景スクロール用タイマー.n現在の値, nBgY[ i ] );
                        //this.tx上背景メイン.t2D描画( CDTXMania.app.Device, ( 3 * nループ幅 ) - this.ct上背景スクロール用タイマー.n現在の値, nBgY[ i ] );
                        //this.tx上背景メイン.t2D描画( CDTXMania.app.Device, ( 4 * nループ幅 ) - this.ct上背景スクロール用タイマー.n現在の値, nBgY[ i ] );
                    }
                    if( this.tx上背景クリアメイン != null )
                    {
                        if( CDTXMania.stage演奏ドラム画面.actGauge.db現在のゲージ値[ 0 ] < 80.0 )
                            this.tx上背景クリアメイン.n透明度 = 0;
                        else
                            this.tx上背景クリアメイン.n透明度 = ( ( this.ct上背景FIFOタイマー.n現在の値 * 0xff ) / 100 );

                        int nループ幅 = 328;
                        this.tx上背景クリアメイン.t2D描画( CDTXMania.app.Device, 0 - this.ct上背景スクロール用タイマー.n現在の値, nBgY[ i ] );
                        this.tx上背景クリアメイン.t2D描画( CDTXMania.app.Device, ( 1 * nループ幅 ) - this.ct上背景スクロール用タイマー.n現在の値, nBgY[ i ] );
                        this.tx上背景クリアメイン.t2D描画( CDTXMania.app.Device, ( 2 * nループ幅 ) - this.ct上背景スクロール用タイマー.n現在の値, nBgY[ i ] );
                        this.tx上背景クリアメイン.t2D描画( CDTXMania.app.Device, ( 3 * nループ幅 ) - this.ct上背景スクロール用タイマー.n現在の値, nBgY[ i ] );
                        this.tx上背景クリアメイン.t2D描画( CDTXMania.app.Device, ( 4 * nループ幅 ) - this.ct上背景スクロール用タイマー.n現在の値, nBgY[ i ] );
                    }
                }




                if( CDTXMania.ConfigIni.nPlayerCount == 1 )
                {
                    {
                        if( this.tx下背景メイン != null )
                        {
                            this.tx下背景メイン.t2D描画( CDTXMania.app.Device, 0, 360 );
                        }
                    }
                    if( CDTXMania.stage演奏ドラム画面.actGauge.db現在のゲージ値[ 0 ] >= 80.0 )
                    {
                        if( this.tx下背景クリアメイン != null && this.tx下背景クリアサブ1 != null )
                        {
                            this.tx下背景クリアメイン.n透明度 = ( ( this.ct上背景FIFOタイマー.n現在の値 * 0xff ) / 100 );
                            this.tx下背景クリアサブ1.n透明度 = ( ( this.ct上背景FIFOタイマー.n現在の値 * 0xff ) / 100 );
                    
                            this.tx下背景クリアメイン.t2D描画( CDTXMania.app.Device, 0, 360 );

                            int nループ幅 = 1257;
                            this.tx下背景クリアサブ1.t2D描画( CDTXMania.app.Device, 0 - this.ct下背景スクロール用タイマー1.n現在の値, 360 );
                            this.tx下背景クリアサブ1.t2D描画( CDTXMania.app.Device, ( 1 * nループ幅 ) - this.ct下背景スクロール用タイマー1.n現在の値, 360 );
                        }
                    }
                }
            }
            return base.On進行描画();
        }

        #region[ private ]
        //-----------------
        private CCounter ct上背景スクロール用タイマー; //上背景のX方向スクロール用
        private CCounter ct下背景スクロール用タイマー1; //下背景パーツ1のX方向スクロール用
        private CCounter ct上背景FIFOタイマー;
        private CTexture tx上背景メイン;
        private CTexture tx上背景クリアメイン;
        private CTexture tx下背景メイン;
        private CTexture tx下背景クリアメイン;
        private CTexture tx下背景クリアサブ1;

        private Dictionary<string, CTexture> dicTexture;
        private EFIFOモード eFadeMode;
        private int nLoopWidth;
        //-----------------
        #endregion
    }
}
