import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private _snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {}

  onSubmit(f: NgForm) {

    const loginObserver = {
      next: (x: any) => {
        console.log(x);
        this._snackBar.open("Welcome!", "Dismiss");
      },
      error: (err: any) => {
        console.log(err);
        this._snackBar.open(err.error, "Dismiss");
      },
    };
    this.authService.login(f.value).subscribe(loginObserver);
  }
}

