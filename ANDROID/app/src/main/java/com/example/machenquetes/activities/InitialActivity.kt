package com.example.machenquetes.activities

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import com.example.machenquetes.R

class InitialActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_initial)
    }

    fun GoLoginActivity(view: View) {
        val telaLogin= Intent(this, LoginActivity::class.java)
        startActivity(telaLogin);
    }

    fun goSignUpActivity(view: View) {
        val telaCadastro = Intent(this, SignupActivity::class.java)
        startActivity(telaCadastro);
    }
}