package com.example.machenquetes.activities

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.view.WindowManager
import android.widget.CheckBox
import android.widget.EditText
import android.widget.Toast
import com.example.machenquetes.R

class SignupActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_signup)
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
    }

    fun SendSignUpData(view: View) {
        val inpNomeCompleto = findViewById<EditText>(R.id.inp_nome).text.toString()
        val inpDataNascimento = findViewById<EditText>(R.id.inp_data).text.toString()
        val inpEmail = findViewById<EditText>(R.id.inp_email).text.toString()
        val inpSenha = findViewById<EditText>(R.id.inp_senha).text.toString()
        val inpConfirmacaoSenha = findViewById<EditText>(R.id.inp_confSenha).text.toString()

        val usuario: Usuario = Usuario(inpNomeCompleto, inpCPF,inpDataNascimento,
            inpEmail,inpSenha, inpTelefone,cbEhConsumidor
        );

        usuario.converterDatanascimento()

        if(inpSenha == inpConfirmacaoSenha && inpSenha != ""){
            preferences.SalvarUsuario("usuario",usuario)
            irTelaConfirmacaoEmail()
        }else{
            Toast.makeText(this, "Senhas não correspondem", Toast.LENGTH_SHORT ).show()
        }
    }

    fun GoToSurveysPage()
    {
        val TelaEmail= Intent(this, TelaEmail::class.java)
        startActivity(TelaEmail);
    }
}