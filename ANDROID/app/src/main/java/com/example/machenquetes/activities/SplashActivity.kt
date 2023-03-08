package com.example.machenquetes.activities

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Handler
import android.os.HandlerThread
import com.example.machenquetes.R

class SplashActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_splash)

        val handlerThread = HandlerThread("background-thread")
        handlerThread.start()
        val handler = Handler(handlerThread.looper)
        handler.postDelayed({
            val intent = Intent(this@SplashActivity, InitialActivity::class.java)
            startActivity(intent)
            handlerThread.quitSafely()
        }, 2500)
    }


}