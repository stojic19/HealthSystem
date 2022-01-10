import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/AuthService/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public form!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private _router: Router,
    private _service: AuthService,
    private _snackBar: MatSnackBar
    ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username:'',
      password:''
    });
  }
  
  submit():void{
   const loginObserver = {
     next: (x:any) => {
        this._snackBar.open("Wellcome","Dismiss");
     },
      error: (err:any) => {
        this._snackBar.open(err.error,"Dismiss");
      }};
   
   this._service.login(this.form.getRawValue()).subscribe(loginObserver);
   //TODO: Id za med record
   this._router.navigate(['/record']);
  }
}
