/// <binding />

var gulp = require("gulp"),
  watch = require('gulp-watch');

gulp.task('usacjj-watcher', function () {
  gulp.watch(["Views/**/*.cshtml"], ['deploy-views']);
  gulp.watch(["Content/*.css"], ['deploy-css']);
});

gulp.task('deploy-views', function () {
  return gulp.src(['Views/**/*.cshtml'])
    .pipe(gulp.dest('C:\\inetpub\\wwwroot\\usacjj\\Website\\Views\\'));
});

gulp.task('deploy-css', function () {
  return gulp.src(['Content/*.css'])
    .pipe(gulp.dest('C:\\inetpub\\wwwroot\\usacjj\\Website\\Content\\css'));
});