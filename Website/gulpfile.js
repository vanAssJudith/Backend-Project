// Basic packages
const gulp = require('gulp');
const browsersync = require('browser-sync').create();

// Style packages
const sass = require('gulp-sass');
const rename = require('gulp-rename');
const postcss = require('gulp-postcss');
const autoprefixer = require('autoprefixer');
const cssnano = require('cssnano');

// HTML packages
const htmlmin = require('gulp-htmlmin');

// const uglify = require('gulp-uglify');
const babel = require('gulp-babel');
const concat = require('gulp-concat');

/* #2 De TASKS / WATCHERS zelf die we aanmaken */
// a We willen een development server opzetten
function browserSync(done) {
	browsersync.init({
		open: false,
		server: {
			baseDir: './dist/',
			open: false // Niet iedere keer dat nieuwe browser venster 😡
		},
		port: 3000
	});
	done();
}
// BrowserSync Reload
function browserSyncReload(done) {
	browsersync.reload();
	done();
}
// b html moet geminified naar de dist-map gezet worden
function minifyHTML() {
	return gulp
		.src('./src/**/*.html')
		.pipe(htmlmin({ collapseWhitespace: true }))
		.pipe(gulp.dest('./dist/'));
}
// c JS willen we samenvoegen, minifien en compatibel maken
// Transpile, concatenate and minify scripts
function scripts() {
	return (
		gulp
			.src(['./src/script/lib/*.js', './src/script/app.js'])
			.pipe(concat('app.min.js'))
			// .pipe(uglify())
			.pipe(gulp.dest('./dist/script/'))
			.pipe(
				babel({
					presets: ['@babel/env']
				})
			)
			.pipe(browsersync.stream())
	);
}

function style() {
	return gulp
		.src('./src/style/screen.scss')
		.pipe(sass())
		.pipe(rename({ suffix: '.min' }))
		.pipe(postcss([autoprefixer(), cssnano()]))
		.on('error', sass.logError)
		.pipe(gulp.dest('./dist/style/'));
}

function watchFiles() {
	gulp.watch('./src/script/**/*.js', gulp.series(scripts, browserSyncReload));
	gulp.watch('./src/style/**/*.scss', gulp.series(style, browserSyncReload));
	gulp.watch(['./src/**/*.html'], gulp.series(minifyHTML, browserSyncReload));
}

// d Watchen van veranderingen
const serve = gulp.parallel(watchFiles, browserSync); // complexere combinatie van tasks...

exports.serve = serve;
exports.script = scripts;
exports.default = serve;
