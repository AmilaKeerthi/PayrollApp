import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UntypedFormControl, Validators, UntypedFormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { NotificationService } from '../../../core/services/notification.service';
import { AuthenticationService } from '../../../core/services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    loginForm!: UntypedFormGroup;
    loading!: boolean;

    constructor(private router: Router,
        private titleService: Title,
        private notificationService: NotificationService,
        private authenticationService: AuthenticationService) {
    }

    ngOnInit() {
        this.titleService.setTitle('Employee-payroll - Login');
        this.authenticationService.logout();
        this.createForm();
    }

    private createForm() {
        const savedUserEmail = localStorage.getItem('savedUserEmail');

        this.loginForm = new UntypedFormGroup({
            email: new UntypedFormControl(savedUserEmail, [Validators.required, Validators.email]),
            password: new UntypedFormControl('', Validators.required),
            rememberMe: new UntypedFormControl(savedUserEmail !== null)
        });
    }

    login() {
        const email = this.loginForm.get('email')?.value;
        const password = this.loginForm.get('password')?.value;
        const rememberMe = this.loginForm.get('rememberMe')?.value;

        this.loading = true;
        this.authenticationService
            .login(email.toLowerCase(), password)
            .subscribe(
              {
                next: (res) => {
                      if (rememberMe) {
                        localStorage.setItem('savedUserEmail', email);
                    } else {
                        localStorage.removeItem('savedUserEmail');
                    }
                    this.router.navigate(['/']);
                },
                error: (err) => {
                       this.notificationService.openSnackBar(err.error.title);
                    this.loading = false;
                },
              }
            );
    }

    resetPassword() {
        this.router.navigate(['/auth/password-reset-request']);
    }
}
